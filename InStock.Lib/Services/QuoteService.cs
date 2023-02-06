﻿using InStock.Lib.DataAccess;
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
            var dbEntity = _repoQuote.Using(x => x.Select(id));

            return dbEntity;
        }

        public QuoteEntity? GetQuote(string symbol)
        {
            var dbEntity = _repoQuote.Using(x => x.Select(symbol));

            return dbEntity;
        }

        public QuoteEntity? GetRecentQuote(string symbol)
        {
            //This is hard coded for now until I figure out how to deal with it properly
            var dbEntity = _repoQuote.Using(x => x.SelectRecent(symbol, DateTime.UtcNow, 5));

            return dbEntity;
        }

        public async Task<QuoteEntity> Add(string symbol)
        {
            var stock = _repoStock.Using(x => x.Select(symbol));

            if (stock == null) throw new Exception("Stock not found - make proper exception here.");

            return await Add(stock);
        }

        public async Task<QuoteEntity> Add(StockEntity stock)
        {
            var existingQuote = GetRecentQuote(stock.Symbol);

            if (existingQuote != null) return existingQuote;

            var stockQuote = await _stockQuoteApi.GetQuote(stock.Symbol);

            //If the quote cannot be found raise exception I suppose?
            if (stockQuote == null) throw new SymbolNotFoundException(stock.Symbol);

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
