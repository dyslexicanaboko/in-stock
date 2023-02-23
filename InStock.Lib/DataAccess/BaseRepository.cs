using InStock.Lib.Services;
using Microsoft.Data.SqlClient;

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
        protected string? ConnectionString;
        protected SqlConnection? Connection;
        protected SqlTransaction? Transaction;

        protected BaseRepository()
        {
            //NOTE: Do not instantiate the connection or transaction objects here. This is mostly for the purposes of DI.
        }

        protected BaseRepository(IAppConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString();
        }

        protected BaseRepository(SqlConnection connection)
        {
            Connection = connection;
            
            ConnectionString = Connection.ConnectionString;
        }

        protected BaseRepository(SqlTransaction transaction)
            : this(transaction.Connection)
        {
            Transaction = transaction;
        }
        
        protected SqlConnection GetConnection()
        {
            if(Connection == null || string.IsNullOrWhiteSpace(Connection.ConnectionString))
                Connection = new SqlConnection(ConnectionString);

            if (Connection.State != System.Data.ConnectionState.Open) Connection.Open();

            return Connection;
        }

        //Not crazy about this
        public void SetTransaction(SqlTransaction transaction)
        {
            Transaction = transaction;
            Connection = Transaction.Connection;

            ConnectionString = Connection.ConnectionString;
        }

        public void Dispose()
        {
            if (Connection == null) return;

            //What happens if a transaction is not committed yet?
            Transaction?.Dispose();

            //The close and/or dispose method more than likely handle the closing if the connection is open
            Connection.Close();
            Connection.Dispose();
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
