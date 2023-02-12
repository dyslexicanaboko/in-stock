using FakeItEasy;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Services;
using NUnit.Framework;

namespace InStock.UnitTesting.ServiceTests
{
    [TestFixture]
    public class PositionServiceTests
        : TestBaseInStock
    {
        private IStockRepository _repoStock;
        private IPositionRepository _repoPosition;
        private IPositionService _service;

        [SetUp]
        public void Setup()
        {
            _repoStock = A.Fake<IStockRepository>();

            _repoPosition = A.Fake<IPositionRepository>();

            _service = new PositionService(_repoPosition, _repoStock);
        }

        [Test]
        public void Add_WhenPositionIsUnique_ThenPositionIsInserted()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(GetSomeStock());
            A.CallTo(() => _repoPosition.Select(A<int>._, A<string>._)).Returns(Enumerable.Empty<PositionEntity>());

            var position = new PositionEntity
            {
                StockId = SomeStockId,
                UserId = SomeUserId
            };

            //Act
            _service.Add(position);

            //Assert
            A.CallTo(() => _repoPosition.Insert(A<PositionEntity>._))
                .MustHaveHappened();
        }

        [Test]
        public void Add_WhenPositionIsNotUnique_ThenPositionExistsAlreadyException()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<int>._)).Returns(GetSomeStock());
            A.CallTo(() => _repoPosition.Select(A<int>._, A<string>._)).Returns(GetSomePositions());

            var position = GetSomePosition();

            //Act/Assert
            Assert.Throws<PositionExistsAlreadyException>(() => _service.Add(position));

            //Assert
            A.CallTo(() => _repoPosition.Insert(A<PositionEntity>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Add_WhenStockIsNotFound_ThenStockNotFoundExceptionIsRaised()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<int>._)).Returns(null);

            var position = GetSomePosition();

            //Act/Assert
            Assert.Throws<StockNotFoundException>(() => _service.Add(position));

            //Assert
            A.CallTo(() => _repoPosition.Insert(A<PositionEntity>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Delete_WhenSymbolCannotBeFound_ThenSymbolNotFoundExceptionIsRaised()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(null);

            //Act

            Assert.Throws<SymbolNotFoundException>(() => _service.Delete(SomeUserId, SomeSymbol));
            
            //Assert
            A.CallTo(() => _repoPosition.Delete(A<int>._, A<string>._))
                .MustNotHaveHappened();
        }
    }
}