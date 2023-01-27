namespace InStock.Lib.DataAccess
{
    public class TransactionManager
        : BaseRepository, ITransactionManager
    {
        private readonly Dictionary<string, object> _repositories;

        //NOTE: I hate this giant eye sore, but I am going to leave it for now until I can fix it
        //I couldn't unit test properly because I was using concrete implementations
        public TransactionManager(
            IEarningsRepository earningsRepository,
            IPositionRepository positionRepository,
            IQuoteRepository quoteRepository,
            IStockRepository stockRepository,
            ITradeRepository tradeRepository,
            IUserRepository userRepository)
            : base()
        {
            _repositories = new Dictionary<string, object>
            {
                { nameof(IEarningsRepository), earningsRepository },
                { nameof(IPositionRepository), positionRepository },
                { nameof(IQuoteRepository), quoteRepository },
                { nameof(IStockRepository), stockRepository },
                { nameof(ITradeRepository), tradeRepository },
                { nameof(IUserRepository), userRepository }
            };
        }

        public void Begin()
        {
            if (Transaction != null)
                throw new InvalidOperationException("Transaction has already begun. Commit the current transaction before starting a new one.");

            Connection = GetConnection();

            Transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            if (Transaction == null)
                throw new InvalidOperationException("Cannot commit a transaction that was never begun. Transaction was null.");

            Transaction.Commit();
            Transaction.Dispose();
            Transaction = null;
        }

        public TRepository GetRepository<TRepository>()
            where TRepository : IRepository
        {
            if (Connection == null || 
                Transaction == null ||
                Connection.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("You must call the Begin() method before access repositories in this context.");

            var t = typeof(TRepository);

            //Keep track of the repositories in use
            var repo = (TRepository)_repositories[t.Name];

            //Set the transaction so it's automatically part of the opened connection and transaction.
            repo.SetTransaction(Transaction);

            return repo;
        }
    }
}
