using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace InStock.Lib.DataAccess
{
    /// <summary>
    /// The Base DAL only exists to provide access to the connection string and for basic ADO.NET functionality
    /// to the other Data Layer implementations.
    /// NuGet Dependencies:
    ///     Microsoft.Data.SqlClient, Version="3.0.0"
    ///     Microsoft.Extensions.Configuration, Version="5.0.0"
    ///     Microsoft.Extensions.Configuration.FileExtensions, Version="5.0.0"
    ///     Microsoft.Extensions.Configuration.Json, Version="5.0.0"
    /// </summary>
    public abstract class BaseRepository
        : IDisposable
    {
        protected string ConnectionString;
        protected SqlConnection? _connection;
        protected SqlTransaction? _transaction;

        //NOTE: Do not instantiate the connection or transaction objects here. This is mostly for the purposes of DI.
        protected BaseRepository()
        {
            ConnectionString = LoadConnectionString();
        }

        protected BaseRepository(SqlConnection connection)
        {
            _connection = connection;
            
            ConnectionString = _connection.ConnectionString;
        }

        protected BaseRepository(SqlTransaction transaction)
            : this(transaction.Connection)
        {
            _transaction = transaction;
        }

        private static string LoadConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("InStock");

            return connectionString!;
        }

        protected SqlConnection GetConnection()
        {
            if(_connection == null) _connection = new SqlConnection(ConnectionString);

            if(_connection.State != System.Data.ConnectionState.Open) _connection.Open();

            return _connection;
        }

        //Not crazy about this
        public void SetTransaction(SqlTransaction transaction)
        {
            _transaction = transaction;
            _connection = _transaction.Connection;

            ConnectionString = _connection.ConnectionString;
        }

        public void Dispose()
        {
            if (_connection == null) return;

            //What happens if a transaction is not committed yet?
            _transaction?.Dispose();

            //The close and/or dispose method more than likely handle the closing if the connection is open
            _connection.Close();
            _connection.Dispose();
        }

        #region Not sure I will use any of this
        /*
        protected void ExecuteNonQuery(string sql, SqlParameter[] parameters)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static object GetScalar(IDataReader reader, string column)
        {
            if (reader == null)
                return null;

            object obj = null;

            while (reader.Read())
                obj = reader[column];

            return obj;
        }

        protected static List<T> ToList<T>(IDataReader reader, Func<IDataReader, T> method) where T : new()
        {
            var lst = new List<T>();

            if (reader == null)
                return lst;

            while (reader.Read())
                lst.Add(method(reader));

            return lst;
        }

        protected IDataReader ExecuteReaderText(string sql, params SqlParameter[] parameters)
        {
            return ExecuteReader(CommandType.Text, sql, parameters);
        }

        protected IDataReader ExecuteReaderSproc(string sql, params SqlParameter[] parameters)
        {
            return ExecuteReader(CommandType.StoredProcedure, sql, parameters);
        }

        private IDataReader ExecuteReader(CommandType commandType, string sql, SqlParameter[] parameters)
        {
            var con = new SqlConnection(ConnectionString);

            con.Open();

            var cmd = new SqlCommand(sql, con);
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        */
        #endregion
    }
}
