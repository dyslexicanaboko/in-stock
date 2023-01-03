namespace InStock.Lib.DataAccess
{
    public class TransactionManager
        : BaseRepository, ITransactionManager
    {
        private readonly Dictionary<string, object> _repositories;

        public TransactionManager()
            : base()
        {
            _repositories = new Dictionary<string, object>();
        }

        public void Begin()
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction has already begun. Commit the current transaction before starting a new one.");

            _connection = GetConnection();

            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Cannot commit a transaction that was never begun. Transaction was null.");

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public T GetRepository<T>()
            where T : BaseRepository, new()
        {
            if (_connection == null || 
                _transaction == null ||
                _connection.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("You must call the Begin() method before access repositories in this context.");

            var t = typeof(T);

            //Keep track of the repositories in use
            if (_repositories.ContainsKey(t.Name)) return (T)_repositories[t.Name];

            //Instantiate the repository, but set the transaction so it's automatically
            //part of the opened connection and transaction.
            var repo = new T();
            repo.SetTransaction(_transaction);

            _repositories.Add(t.Name, repo);

            return repo;
        }
    }
}
