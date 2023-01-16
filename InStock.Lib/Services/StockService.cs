using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Services.ApiClient;

namespace InStock.Lib.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _repoStock;
        private readonly ITransactionManager _tran;
        private readonly IStockQuoteApiService _service;

        public StockService(
            IStockRepository repository,
            ITransactionManager transactionManager,
            IStockQuoteApiService service)
        {
            _repoStock = repository;

            _tran = transactionManager;

            _service = service;
        }

        public StockEntity? GetStock(int id)
        {
            var dbEntity = _repoStock.Using(x => x.Select(id));

            return dbEntity;
        }

        public StockEntity? GetStock(string symbol)
        {
            var dbEntity = _repoStock.Using(x => x.Select(symbol));

            return dbEntity;
        }

        public async Task<StockEntity> Add(string symbol)
        {
            var dbEntity = _repoStock.Using(x => x.Select(symbol));

            if (dbEntity != null) return dbEntity;

            //Needs to hit the API to get the Name at the minimum
            var stockQuote = await _service.GetQuote(symbol);

            //If the quote cannot be found raise exception I suppose?
            if (stockQuote == null) throw new SymbolNotFoundException(symbol);

            var entity = new StockEntity
            {
                Symbol = symbol,
                Name = stockQuote.Name
            };

            using (_tran)
            {
                _tran.Begin();

                entity.StockId = _tran.GetRepository<StockRepository>().Insert(entity);

                //I am not sure I want to keep this here
                var quote = new QuoteEntity
                {
                    StockId = entity.StockId,
                    Date = stockQuote.Date,
                    Price = stockQuote.Price,
                    Volume = stockQuote.Volume
                };

                _tran.GetRepository<QuoteRepository>().Insert(quote);

                _tran.Commit();
            }

            return entity;
        }
    }
}
