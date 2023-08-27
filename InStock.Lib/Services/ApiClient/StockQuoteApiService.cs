using NodaTime;
using YahooQuotesApi;

namespace InStock.Lib.Services.ApiClient
{
  public class StockQuoteApiService
    : IStockQuoteApiService
  {
    private readonly IDateTimeService _dateTimeService;

    private AmericanHolidaySearch _holidays;

    private int _year;

    public StockQuoteApiService(IDateTimeService dateTimeService)
    {
      _dateTimeService = dateTimeService;

      _year = dateTimeService.UtcNow.Year;

      _holidays = new AmericanHolidaySearch(_year);
    }

    public async Task<StockQuoteModel?> GetQuote(string symbol)
    {
      Security? item;

      var dtm = GetMostRecentWeekday();

      var quotes = new YahooQuotesBuilder()
        .WithHistoryStartDate(Instant.FromDateTimeUtc(dtm.ToUniversalTime()))
        .Build();

      try
      {
        item = await quotes.GetAsync(symbol, Histories.PriceHistory) ?? throw new ArgumentException("Unknown symbol");
      }
      catch (Exception ex)
      {
        //System.Net.HttpStatusCode.TooManyRequests 429 can be thrown
        throw;
      }

      var history = item.PriceHistory.Value.First();

      var sq = new StockQuoteModel(
        new DateTime(history.Date.Year, history.Date.Month, history.Date.Day),
        symbol,
        item.LongName,

        //Bid = item.Bid,
        //Ask = item.Ask,
        history.Close,
        history.Volume
      );

      return sq;
    }

    public DateTime GetMostRecentWeekday()
    {
      var dtm = _dateTimeService.Now.Date;

      //Edge case - if the year changes during use for any reason, the holidays need to be recalculated
      if (dtm.Year != _year)
      {
        _year = dtm.Year;

        _holidays = new AmericanHolidaySearch(_year);
      }

      //While the provided date is a holiday, roll the date back to last known non-holiday
      while (_holidays.IsHoliday(dtm, out _)) dtm = dtm.AddDays(-1);

      //If it isn't a weekend then escape
      if (dtm.DayOfWeek != DayOfWeek.Saturday && dtm.DayOfWeek != DayOfWeek.Sunday) return dtm;

      //Roll it back to Friday if it is a weekend day
      while (dtm.DayOfWeek != DayOfWeek.Friday) dtm = dtm.AddDays(-1);

      return dtm;
    }
  }
}
