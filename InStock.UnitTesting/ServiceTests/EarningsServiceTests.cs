using FakeItEasy;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Services;
using NUnit.Framework;

namespace InStock.UnitTesting.ServiceTests
{
    [TestFixture]
    public class EarningsServiceTests
        : TestBaseInStock
    {
        private IStockRepository _repoStock;
        private IEarningsRepository _repoEarnings;
        private IEarningsService _service;

        [SetUp]
        public void Setup()
        {
            _repoStock = A.Fake<IStockRepository>();

            _repoEarnings = A.Fake<IEarningsRepository>();

            _service = new EarningsService(_repoEarnings);
        }

        [Test]
        public void Add_WhenEarningsIsUnique_ThenEarningsIsInserted()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<IList<int>>._)).Returns(new List<StockEntity> { GetSomeStock() });
            //A.CallTo(() => _repoEarnings.Select(A<int>._, A<string>._)).Returns(Enumerable.Empty<EarningsEntity>());

            //Act
            var actual = _service.Add(GetSomeEarnings());

            //Assert
            A.CallTo(() => _repoEarnings.Insert(A<EarningsEntity>._))
                .MustHaveHappened();
        }

        [Test]
        public void Add_WhenEarningsIsNotUnique_ThenEarningsExistsAlreadyException()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<IList<int>>._)).Returns(new List<StockEntity> { GetSomeStock() });
            //A.CallTo(() => _repoEarnings.Select(A<int>._, A<string>._)).Returns(Enumerable.Empty<EarningsEntity>());

            //Act/Assert
            Assert.Throws<EarningsExistsAlreadyException>(() => _service.Add(GetSomeEarnings()));

            A.CallTo(() => _repoEarnings.Insert(A<EarningsEntity>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Add_WhenStockIsNotFound_ThenStockNotFoundExceptionIsRaised()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<int>._)).Returns(null);

            var earnings = GetSomeEarnings();

            //Act/Assert
            Assert.Throws<StockNotFoundException>(() => _service.Add(earnings));

            //Assert
            A.CallTo(() => _repoEarnings.Insert(A<EarningsEntity>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Delete_WhenSymbolCannotBeFound_ThenSymbolNotFoundExceptionIsRaised()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(null);

            //Act
            Assert.Throws<SymbolNotFoundException>(() => _service.Delete(SomeSymbol));
            
            //Assert
            A.CallTo(() => _repoEarnings.Delete(A<string>._))
                .MustNotHaveHappened();
        }
    }
}
