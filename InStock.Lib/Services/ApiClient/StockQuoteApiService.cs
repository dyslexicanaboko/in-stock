using NodaTime;
using YahooQuotesApi;

namespace InStock.Lib.Services.ApiClient
{
    public class StockQuoteApiService
        : IStockQuoteApiService
    {
        private int _year;
        private AmericanHolidaySearch _holidays;

        public StockQuoteApiService()
        {
            _year = DateTime.UtcNow.Year;

            _holidays = new AmericanHolidaySearch(_year);
        }

        //TODO: Need to handle after hours quotes - scenario:
        //It's 01/03/2023 00:00 which is Tuesday, but the markets don't open until 09:30 ET
        public async Task<StockQuoteModel> GetQuote(string symbol)
        {
            var dtm = GetMostRecentWeekday();

            var quotes = new YahooQuotesBuilder()
            .WithHistoryStartDate(Instant.FromDateTimeUtc(dtm.ToUniversalTime()))
            .Build();

            var item = await quotes.GetAsync(symbol, Histories.PriceHistory) ?? throw new ArgumentException("Unknown symbol");

            var history = item.PriceHistory.Value.First();

            var sq = new StockQuoteModel
            {
                Symbol = symbol,
                Name = item.LongName,
                //Bid = item.Bid,
                //Ask = item.Ask,
                Date = new DateTime(history.Date.Year, history.Date.Month, history.Date.Day),
                Price = history.Close,
                Volume = history.Volume
            };

            return sq;
        }

        public DateTime GetMostRecentWeekday()
        {
            var dtm = DateTime.Now.Date;

            //Edge case - if the year changes during use for any reason, the holidays need to be recalculated
            if (dtm.Year != _year)
            {
                _year = dtm.Year;

                _holidays = new AmericanHolidaySearch(_year);
            }

            //While the provided date is a holiday, roll the date back to last known non-holiday
            while (_holidays.IsHoliday(dtm, out var _)) dtm = dtm.AddDays(-1);

            //If it isn't a weekend then escape
            if (dtm.DayOfWeek != DayOfWeek.Saturday && dtm.DayOfWeek != DayOfWeek.Sunday) return dtm;

            //Roll it back to Friday if it is a weekend day
            while (dtm.DayOfWeek != DayOfWeek.Friday) dtm = dtm.AddDays(-1);

            return dtm;
        }
    }
}
