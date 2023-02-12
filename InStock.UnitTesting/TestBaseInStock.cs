using InStock.Lib.Entities;
using InStock.Lib.Services.ApiClient;

namespace InStock.UnitTesting
{
    public abstract class TestBaseInStock
        : TestBase
    {
        protected int SomeUserId = 1;
        protected string SomeSymbol = "FLDUH";
        private const int SomeStockId = 7777777;

        public StockEntity GetSomeStock()
        {
            return new StockEntity
            {
                Symbol = SomeSymbol,
                Name = "FLoriDUHH",
                Notes = "Florida sucks",
                StockId = SomeStockId
            };
        }

        public QuoteEntity GetSomeQuote()
        {
            return new QuoteEntity
            {
                QuoteId = 1,
                StockId = SomeStockId,
                Date = TodayUtc,
                Price = 35.81,
                Volume = 2000000
            };
        }

        public StockQuoteModel GetSomeStockQuote()
        {
            var e = GetSomeStock();

            return new StockQuoteModel(TodayUtc, e.Symbol, e.Name, 0.0001, 3);
        }
    }
}
