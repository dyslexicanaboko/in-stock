using InStock.Lib.Entities;
using NUnit.Framework;

namespace InStock.UnitTesting.EntityTests
{
    [TestFixture]
    public class TradeEntityTests
        : CompareTestBase<TradeEntity>
    {
        [Test]
        public override void Objects_are_not_equal()
        {
            //Arrange
            var left = GetFilledObject();

            var right = new TradeEntity
            {
                TradeId = 2,
                StockId = 2,
                UserId = 2,
                Price = 2,
                Quantity = 2,
                TradeType = TradeType.Sell,
                ExecutionDateUtc = TomorrowUtc
            };

            //Act / Assert
            AssertAreNotEqual(left, right);
        }

        protected override TradeEntity GetFilledObject() => new()
            {
                TradeId = 1,
                StockId = 1,
                UserId = 1,
                Price = 1,
                Quantity = 1,
                TradeType = TradeType.Buy,
                ExecutionDateUtc = TodayUtc
            };
    }
}
