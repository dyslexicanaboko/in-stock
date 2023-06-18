using CommunityToolkit.Diagnostics;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Services.ApiClient;

namespace InStock.Lib.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IStockRepository _repoStock;
        private readonly IQuoteRepository _repoQuote;
        private readonly IStockQuoteApiService _stockQuoteApi;

        public QuoteService(
            IQuoteRepository repoQuote,
            IStockRepository repoStock,
            IStockQuoteApiService stockQuoteApi)
        {
            _repoQuote = repoQuote;

            _repoStock = repoStock;

            _stockQuoteApi = stockQuoteApi;
        }

        public QuoteEntity? GetQuote(int id)
        {
            Guard.IsGreaterThan(id, 0, nameof(id));

            var dbEntity = _repoQuote.Using(x => x.Select(id));

            return dbEntity;
        }

        public QuoteEntity? GetQuote(string symbol)
        {
            Guard.IsNotNullOrWhiteSpace(symbol, nameof(symbol));
            
            var dbEntity = _repoQuote.Using(x => x.Select(symbol));

            return dbEntity;
        }

        public QuoteEntity? GetRecentQuote(string symbol)
        {
            Guard.IsNotNullOrWhiteSpace(symbol, nameof(symbol));
            
            //This is hard coded for now until I figure out how to deal with it properly
            var dbEntity = _repoQuote.Using(x => x.SelectRecent(symbol, DateTime.UtcNow, 5));

            return dbEntity;
        }

        public async Task<QuoteEntity> Add(string symbol)
        {
            Guard.IsNotNullOrWhiteSpace(symbol, nameof(symbol));
            
            var stock = _repoStock.Using(x => x.Select(symbol));

            if (stock == null) throw NotFoundExceptions.Stock(symbol);

            return await Add(stock);
        }

        public async Task<QuoteEntity> Add(StockEntity stock)
        {
            var existingQuote = GetRecentQuote(stock.Symbol);

            if (existingQuote != null) return existingQuote;

            var stockQuote = await _stockQuoteApi.GetQuote(stock.Symbol);

            //If the quote cannot be found raise exception I suppose?
            if (stockQuote == null) throw NotFoundExceptions.Symbol(stock.Symbol);

            var quote = new QuoteEntity
            {
                StockId = stock.StockId,
                Date = stockQuote.Date,
                Price = stockQuote.Price,
                Volume = stockQuote.Volume
            };

            quote.QuoteId = _repoQuote.Using(x => x.Insert(quote));

            return quote;
        }
    }
}
