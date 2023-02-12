using FakeItEasy;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Services;
using InStock.Lib.Services.ApiClient;
using NUnit.Framework;

namespace InStock.UnitTesting.ServiceTests
{
    [TestFixture]
    public class StockServiceTests
        : TestBaseInStock
    {
        private IStockRepository _repoStock;
        private IQuoteRepository _repoQuote;
        private ITransactionManager _tran;
        private IStockQuoteApiService _stockQuoteApi;
        private IStockService _service;

        [SetUp]
        public void Setup()
        {
            _repoStock = A.Fake<IStockRepository>();
            
            _repoQuote = A.Fake<IQuoteRepository>();

            _tran = A.Fake<ITransactionManager>();

            _stockQuoteApi = A.Fake<IStockQuoteApiService>();

            _service = new StockService(_repoStock, _tran, _stockQuoteApi);
        }

        [Test]
        public async Task Add_WhenStockExistsAlready_ThenExistingStockIsReturned()
        {
            //Arrange
            var stockQuoteApi = A.Fake<IStockQuoteApiService>(); //Declaring locally, was having a clash with a different test


            var expected = GetSomeStock();

            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(expected);

            //Act
            var actual = await _service.Add(SomeSymbol);

            //Assert
            Assert.AreEqual(expected.StockId, actual.StockId);
            
            A.CallTo(() => stockQuoteApi.GetQuote(A<string>._))
                .MustNotHaveHappened();
        }

        [Test]
        public async Task Add_WhenStockDoesNotExist_ThenStockAdded()
        {
            //Arrange
            var expected = GetSomeStock();

            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(null);
            A.CallTo(() => _repoStock.Insert(A<StockEntity>._)).Returns(expected.StockId);
            A.CallTo(() => _stockQuoteApi.GetQuote(A<string>._)).Returns(GetSomeStockQuote());
            A.CallTo(() => _tran.GetRepository<IStockRepository>()).Returns(_repoStock);
            A.CallTo(() => _tran.GetRepository<IQuoteRepository>()).Returns(_repoQuote);

            //Act
            var actual = await _service.Add(SomeSymbol);

            //Assert
            Assert.AreEqual(expected.StockId, actual.StockId);
            A.CallTo(() => _tran.Commit())
                .MustHaveHappened();
        }
    }
}