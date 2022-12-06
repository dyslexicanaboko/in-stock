using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;

namespace InStock.Lib.Services
{
    public class StockService
    {
        private readonly IStockRepository _repoStock;
        private readonly IQuoteRepository _repoQuote;
        private readonly IStockQuoteApiService _service;

        public StockService(
            IStockRepository repository,
            IQuoteRepository repoQuote,
            IStockQuoteApiService service)
        {
            _repoStock = repository;

            _repoQuote = repoQuote;

            _service = service;
        }

        public StockEntity Add(string symbol) 
        {
            var dbEntity = _repoStock.Select(symbol);

            if (dbEntity != null) return dbEntity;

            //Needs to hit the API to get the Name at the minimum
            var stockQuote = _service.GetQuote(symbol);

            //If the quote cannot be found raise exception I suppose?
            if (stockQuote == null) throw new SymbolNotFoundException(symbol);

            var entity = new StockEntity
            {
                Symbol = symbol,
                Name = stockQuote.Name
            };

            //A transaction needs to be opened to cover the repo work
            entity.StockId = _repoStock.Insert(entity);

            //I am not sure I want to keep this here
            var quote = new QuoteEntity
            {
                StockId = entity.StockId,
                Date = stockQuote.Date,
                Price = stockQuote.Price,
                Volume= stockQuote.Volume
            };

            _repoQuote.Insert(quote);

            return entity;
        }
    }
}
