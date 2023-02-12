using InStock.Lib.Entities;
using NUnit.Framework;

namespace InStock.UnitTesting.EntityTests
{
    [TestFixture]
    public class PositionEntityTests
        : CompareTestBase<PositionEntity>
    {
        [Test]
        public override void Objects_are_not_equal()
        {
            //Arrange
            var left = GetFilledObject();

            var right = new PositionEntity
            {
                PositionId = 2,
                StockId = 2,
                UserId = 2,
                Price = 2,
                Quantity = 2,
                DateOpened = TomorrowUtc
            };

            //Act / Assert
            AssertAreNotEqual(left, right);
        }

        protected override PositionEntity GetFilledObject() => new()
            {
                PositionId = 1,
                StockId = 1,
                UserId = 1,
                Price = 1,
                Quantity = 1,
                DateOpened = TodayUtc
            };
    }
}
