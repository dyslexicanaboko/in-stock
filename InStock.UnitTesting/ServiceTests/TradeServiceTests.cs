using FakeItEasy;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using NUnit.Framework;

namespace InStock.UnitTesting.ServiceTests
{
    [TestFixture]
    public class TradeServiceTests
        : TestBaseInStock
    {
        private IStockRepository _repoStock;
        private ITradeRepository _repoTrade;
        private ITradeService _service;

        [SetUp]
        public void Setup()
        {
            _repoStock = A.Fake<IStockRepository>();

            _repoTrade = A.Fake<ITradeRepository>();

            _service = new TradeService(_repoTrade, _repoStock);
        }

        [Test]
        public void Add_WhenTradeIsUnique_ThenTradeIsInserted()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<IList<int>>._)).Returns(new List<StockEntity> { GetSomeStock() });
            A.CallTo(() => _repoTrade.Select(A<int>._, A<string>._)).Returns(Enumerable.Empty<TradeEntity>());

            //Act
            var actual = _service.Add(GetSomeTrade());

            //Assert
            Assert.AreEqual(1, actual.Count);

            var a = actual.Single();

            Assert.IsTrue(a.IsSuccessful);

            A.CallTo(() => _repoTrade.Insert(A<TradeEntity>._))
                .MustHaveHappened();
        }

        [Test]
        public void Add_WhenTradeIsNotUnique_ThenTradeExistsAlreadyException()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<IList<int>>._)).Returns(new List<StockEntity> { GetSomeStock() });
            A.CallTo(() => _repoTrade.Select(A<int>._, A<string>._)).Returns(GetSomeTrades());

            var position = GetSomeTrade();

            //Act/Assert
            var actual = _service.Add(position);

            //Assert
            Assert.AreEqual(1, actual.Count);

            var a = actual.Single();

            Assert.IsFalse(a.IsSuccessful);
            Assert.IsTrue(a.Exception is TradeExistsAlreadyException);

            A.CallTo(() => _repoTrade.Insert(A<TradeEntity>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Add_WhenStockIsNotFound_ThenStockNotFoundExceptionIsRaised()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<int>._)).Returns(null);

            var position = GetSomeTrade();

            //Act/Assert
            var actual = _service.Add(position);

            //Assert
            Assert.AreEqual(1, actual.Count);
            
            var a = actual.Single();

            Assert.IsFalse(a.IsSuccessful);
            AssertIsStockIdNotFoundException(a.Exception);

            A.CallTo(() => _repoTrade.Insert(A<TradeEntity>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Delete_WhenSymbolCannotBeFound_ThenSymbolNotFoundExceptionIsRaised()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(null);

            //Act

            AssertThrowsSymbolNotFoundException(() => _service.Delete(SomeUserId, SomeSymbol));
            
            //Assert
            A.CallTo(() => _repoTrade.Delete(A<int>._, A<string>._))
                .MustNotHaveHappened();
        }
    }
}