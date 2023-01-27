using InStock.Lib.Entities;
using InStock.Lib.Services.ApiClient;

namespace InStock.UnitTesting
{
    public abstract class TestBaseInStock
        : TestBase
    {
        public string SomeSymbol = "FLDUH";

        public StockEntity GetSomeStock()
        {
            return new StockEntity
            {
                Symbol = SomeSymbol,
                Name = "FLoriDUHH",
                Notes = "Florida sucks",
                StockId = 7777777
            };
        }

        public StockQuoteModel GetSomeStockQuote()
        {
            var e = GetSomeStock();

            return new StockQuoteModel(TodayUtc, e.Symbol, e.Name, 0.0001, 3);
        }
    }
}
