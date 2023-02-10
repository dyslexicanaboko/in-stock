using FakeItEasy;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Services;
using InStock.Lib.Services.ApiClient;
using InStock.UnitTesting;
using NUnit.Framework;

namespace InQuote.UnitTesting.ServiceTests
{
    [TestFixture]
    public class QuoteServiceTests
        : TestBaseInStock
    {
        private readonly IStockRepository _repoStock;
        private readonly IQuoteRepository _repoQuote;
        private readonly IStockQuoteApiService _stockQuoteApi;
        private readonly IQuoteService _service;

        public QuoteServiceTests()
        {
            _repoStock = A.Fake<IStockRepository>();
            
            _repoQuote = A.Fake<IQuoteRepository>();

            _stockQuoteApi = A.Fake<IStockQuoteApiService>();

            _service = new QuoteService(_repoQuote, _repoStock, _stockQuoteApi);
        }

        [Test]
        public async Task Add_WhenQuoteExistsAlready_ThenExistingQuoteIsReturned()
        {
            //Arrange
            var stock = GetSomeStock();
            var expected = GetSomeQuote();

            A.CallTo(() => _repoQuote.SelectRecent(A<string>._, A<DateTime>._, A<int>._)).Returns(expected);
            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(stock);

            //Act
            var actual = await _service.Add(SomeSymbol);

            //Assert
            Assert.AreEqual(expected.QuoteId, actual.QuoteId);
            A.CallTo(() => _stockQuoteApi.GetQuote(A<string>._))
                .MustNotHaveHappened();
        }

        [Test]
        public Task Add_WhenStockDoesNotExist_ThenStockNotFoundExceptionIsThrown()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(null);

            
            //Act/Assert
            Assert.ThrowsAsync<StockNotFoundException>(() => _service.Add(SomeSymbol));

            return Task.CompletedTask;
        }

        [Test]
        public Task Add_WhenStockQuoteNotFound_ThenSymbolNotFoundExceptionIsThrown()
        {
            //Arrange
            A.CallTo(() => _repoStock.Select(A<string>._)).Returns(GetSomeStock());
            A.CallTo(() => _repoQuote.SelectRecent(A<string>._, A<DateTime>._, A<int>._)).Returns(null);
            A.CallTo(() => _stockQuoteApi.GetQuote(A<string>._)).Returns((StockQuoteModel?)null);

            //Act/Assert
            Assert.ThrowsAsync<SymbolNotFoundException>(() => _service.Add(SomeSymbol));

            return Task.CompletedTask;
        }
    }
}