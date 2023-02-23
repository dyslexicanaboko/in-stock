using FakeItEasy;
using InStock.Lib.Services;
using InStock.Lib.Services.ApiClient;
using NUnit.Framework;

namespace InStock.IntegrationTesting.ServiceTests
{
    [TestFixture]
    public class StockQuoteApiServiceTests
        : TestBaseInStock
    {
        private IStockQuoteApiService _stockQuoteApi;
        private AmericanHolidaySearch _holidays;
        private IDateTimeService _dateTimeService;

        [SetUp]
        public void Setup()
        {
            _dateTimeService = A.Fake<IDateTimeService>();

            A.CallTo(() => _dateTimeService.Now).Returns(new DateTime(2022,1,1));
            A.CallTo(() => _dateTimeService.UtcNow).Returns(new DateTime(2022,1,1));

            _holidays = new AmericanHolidaySearch(_dateTimeService.UtcNow.Year);

            _stockQuoteApi = new StockQuoteApiService(_dateTimeService);
        }

        //https://us-public-holidays.com/us-banks/2022
        [TestCase("January 17 2022", "01/14/2022")]
        [TestCase("February 21 2022", "02/18/2022")]
        [TestCase("May 30 2022", "05/27/2022")]
        [TestCase("June 20 2022", "06/17/2022")]
        [TestCase("July 4 2022", "07/1/2022")]
        [TestCase("September 5 2022", "09/2/2022")]
        [TestCase("October 10 2022", "10/7/2022")]
        [TestCase("November 11 2022", "11/10/2022")]
        [TestCase("November 24 2022", "11/23/2022")]
        [TestCase("December 26 2022", "12/23/2022")]
        public void GetMostRecentWeekday_ShouldReturnLastWeekday_WhenGivenClosedMarketDate(string holiday, string expectedDate)
        {
            var dtm = Convert.ToDateTime(holiday);
            var expected = Convert.ToDateTime(expectedDate);

            A.CallTo(() => _dateTimeService.Now).Returns(dtm);
            
            var actual = _stockQuoteApi.GetMostRecentWeekday();

            Assert.AreEqual(expected, actual);
        }
    }
}