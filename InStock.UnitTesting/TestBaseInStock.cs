using CommunityToolkit.Diagnostics;
using InStock.Lib.Entities;
using InStock.Lib.Services;
using InStock.Lib.Services.ApiClient;

namespace InStock.UnitTesting
{
    public abstract class TestBaseInStock
        : TestBase
    {
        protected int SomeUserId = 1;
        protected string SomeSymbol = "FLDUH";
        protected const int SomeStockId = 7777777;

        protected StockEntity GetSomeStock()
        {
            return new StockEntity
            {
                Symbol = SomeSymbol,
                Name = "FLoriDUHH",
                Notes = "Florida sucks",
                StockId = SomeStockId
            };
        }

        protected QuoteEntity GetSomeQuote()
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

        protected StockQuoteModel GetSomeStockQuote()
        {
            var e = GetSomeStock();

            return new StockQuoteModel(TodayUtc, e.Symbol, e.Name, 0.0001, 3);
        }

        protected List<PositionEntity> GetSomePositions(int count = 3)
        {
            var lst = new List<PositionEntity>(count);

            for (var i = 1; i <= count; i++)
            {
                lst.Add(GetSomePosition(i));
            }

            return lst;
        }

        protected PositionEntity GetSomePosition(int increment = 1)
        {
            Guard.IsGreaterThan(increment, 0, nameof(increment));

            return new PositionEntity
            {
                PositionId = increment,
                StockId = SomeStockId,
                UserId = SomeUserId,
                Price = increment,
                Quantity = increment,
                DateOpened = TodayUtc.AddDays(-increment)
            };
        }

        protected IList<EarningsEntity> GetMultipleEarnings(int count)
        {
            var lst = new List<EarningsEntity>(count);

            for (var i = 1; i <= count; i++)
            {
                lst.Add(GetSomeEarnings(i));
            }

            return lst;
        }

        protected EarningsEntity GetSomeEarnings(int increment = 1)
        {
            return new EarningsEntity
            {
                EarningsId = increment,
                StockId = SomeStockId,
                Date = EarningsService.GetYearAgnosticDate(new DateTime(1, 1, increment)),
                Order = increment
            };
        }
    }
}
