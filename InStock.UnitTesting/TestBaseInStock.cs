using CommunityToolkit.Diagnostics;
using FakeItEasy;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Services;
using InStock.Lib.Services.ApiClient;
using NUnit.Framework;

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

        protected List<PositionEntity> GetSomePositions(int count = 3)
        {
            var lst = new List<PositionEntity>(count);

            for (var i = 1; i <= count; i++)
            {
                lst.Add(GetSomePosition(i));
            }

            return lst;
        }

        protected TradeEntity GetSomeTrade(int increment = 1, TradeType tradeType = TradeType.Buy)
        {
            Guard.IsGreaterThan(increment, 0, nameof(increment));

            return new TradeEntity
            {
                TradeId = increment,
                StockId = SomeStockId,
                UserId = SomeUserId,
                TradeType = tradeType,
                Price = increment,
                Quantity = increment,
                ExecutionDate = TodayUtc.AddDays(-increment)
            };
        }

        protected List<TradeEntity> GetSomeTrades(int count = 3)
        {
            var lst = new List<TradeEntity>(count);

            for (var i = 1; i <= count; i++)
            {
                lst.Add(GetSomeTrade(i));
            }

            return lst;
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

        protected void AssertThrowsStockIdNotFoundException(TestDelegate testMethod)
            => AssertThrowsNotFoundException(testMethod, ErrorCodes.NotFound.StockById);

        protected void AssertThrowsSymbolNotFoundException(TestDelegate testMethod)
            => AssertThrowsNotFoundException(testMethod, ErrorCodes.NotFound.Symbol);
        
        private void AssertThrowsNotFoundException(TestDelegate testMethod, int errorCode)
        {
            var ex = Assert.Throws<NotFoundException>(testMethod);
         
            Assert.AreEqual(errorCode, ex.ErrorCode, "Unexpected error code.");
        }

        protected Task AssertThrowsStockIdNotFoundExceptionAsync(AsyncTestDelegate testMethod)
            => AssertThrowsNotFoundExceptionAsync(testMethod, ErrorCodes.NotFound.StockById);

        protected Task AssertThrowsStockSymbolNotFoundExceptionAsync(AsyncTestDelegate testMethod)
            => AssertThrowsNotFoundExceptionAsync(testMethod, ErrorCodes.NotFound.StockBySymbol);

        protected Task AssertThrowsSymbolNotFoundExceptionAsync(AsyncTestDelegate testMethod)
            => AssertThrowsNotFoundExceptionAsync(testMethod, ErrorCodes.NotFound.Symbol);

        private Task AssertThrowsNotFoundExceptionAsync(AsyncTestDelegate testMethod, int errorCode)
        {
            var ex = Assert.ThrowsAsync<NotFoundException>(testMethod);

            Assert.AreEqual(errorCode, ex.ErrorCode, "Unexpected error code.");

            return Task.CompletedTask;
        }

        protected void AssertIsStockIdNotFoundException(Exception ex)
            => AssertIsNotFoundException(ex, ErrorCodes.NotFound.StockById);

        protected void AssertIsSymbolNotFoundException(Exception ex)
            => AssertIsNotFoundException(ex, ErrorCodes.NotFound.Symbol);

        private void AssertIsNotFoundException(Exception ex, int errorCode)
        {
            Assert.IsInstanceOf<NotFoundException>(ex);

            var nfbe = (NotFoundException)ex;

            Assert.AreEqual(errorCode, nfbe.ErrorCode, "Unexpected error code.");
        }
    }
}
