using FakeItEasy;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using NUnit.Framework;
using YahooQuotesApi;

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

            _service = new EarningsService(_repoEarnings, _repoStock, new EarningsMapper());
        }

        [Test]
        public void Add_WhenEarningsIsUnique_ThenEarningsIsInserted()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<IList<int>>._)).Returns(AsList(GetSomeStock()));

            var existing = GetSomeEarnings();
            existing.Date = existing.Date.AddDays(1);
            existing.Order++;

            A.CallTo(() => _repoEarnings.SelectAll(A<int>._, null)).Returns(AsList(existing));

            //Act
            _service.Add(GetSomeEarnings());

            //Assert
            A.CallTo(() => _repoEarnings.Insert(A<EarningsEntity>._))
                .MustHaveHappened();
        }

        [Test]
        public void Add_WhenEarningsOrderIsNotUnique_ThenEarningsExistsAlreadyException()
            => Add_WhenEarningsXIsNotUnique_ThenEarningsExistsAlreadyException((entity) =>
                entity.Date = entity.Date.AddDays(1)); //Date changed, order remains the same

        [Test]
        public void Add_WhenEarningsDateIsNotUnique_ThenEarningsExistsAlreadyException()
            => Add_WhenEarningsXIsNotUnique_ThenEarningsExistsAlreadyException((entity) =>
                entity.Order++); //Order changed, date remains the same

        public void Add_WhenEarningsXIsNotUnique_ThenEarningsExistsAlreadyException(Action<EarningsEntity> modifyEarnings)
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<IList<int>>._)).Returns(AsList(GetSomeStock()));
            A.CallTo(() => _repoEarnings.SelectAll(A<int>._, null)).Returns(AsList(GetSomeEarnings()));

            var entity = GetSomeEarnings();
            
            modifyEarnings(entity);

            //Act/Assert
            var result = _service.Add(entity);

            var actual = result.Single();

            //Assert
            Assert.IsFalse(actual.IsSuccessful);

            Assert.IsTrue(actual.Exception is EarningsExistsAlreadyException);

            A.CallTo(() => _repoEarnings.Insert(A<EarningsEntity>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Edit_WhenEarningsDateIsNotUnique_ThenEarningsExistsAlreadyException()
            => Edit_WhenEarningsXIsNotUnique_ThenEarningsExistsAlreadyException((entity) =>
                entity.Date = entity.Date.AddDays(-1));

        [Test]
        public void Edit_WhenEarningsOrderIsNotUnique_ThenEarningsExistsAlreadyException()
            => Edit_WhenEarningsXIsNotUnique_ThenEarningsExistsAlreadyException((entity) =>
                entity.Order--);

        public void Edit_WhenEarningsXIsNotUnique_ThenEarningsExistsAlreadyException(Action<EarningsEntity> modifyEarnings)
        {
            //Arrange
            var existingEarnings = GetMultipleEarnings(3); //1 through 3
            var entity = GetSomeEarnings(4); //Four on its own

            modifyEarnings(entity); //Modify 4th

            A.CallTo(() => _repoStock.Select(A<IList<int>>._)).Returns(AsList(GetSomeStock()));
            A.CallTo(() => _repoEarnings.SelectAll(A<int>._, A<int>._)).Returns(existingEarnings);

            //Act/Assert
            Assert.Throws<EarningsExistsAlreadyException>(() => _service.Edit(entity));

            A.CallTo(() => _repoEarnings.Update(A<EarningsEntity>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Add_WhenStockIsNotFound_ThenStockNotFoundExceptionIsRaised()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<int>._)).Returns(null);
            
            //Act
            var result = _service.Add(GetSomeEarnings());

            var actual = result.Single();

            //Assert
            Assert.IsFalse(actual.IsSuccessful);

            AssertIsStockIdNotFoundException(actual.Exception);

            A.CallTo(() => _repoEarnings.Insert(A<EarningsEntity>._))
                .MustNotHaveHappened();
        }

        [Test]
        public void Add_WhenStockHasEnoughEarningsEntries_ThenMaxEntriesExceptionIsRaised()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<IList<int>>._)).Returns(AsList(GetSomeStock()));
            A.CallTo(() => _repoEarnings.SelectAll(A<int>._, null)).Returns(GetMultipleEarnings(EarningsService.MaxEntries));

            //Act/Assert
            var result = _service.Add(GetSomeEarnings());

            var actual = result.Single();

            //Assert
            Assert.IsFalse(actual.IsSuccessful);

            Assert.IsTrue(actual.Exception is MaxEntriesException);

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
            AssertThrowsSymbolNotFoundException(() => _service.Delete(SomeSymbol));
            
            //Assert
            A.CallTo(() => _repoEarnings.Delete(A<string>._))
                .MustNotHaveHappened();
        }
    }
}
