using InStock.Lib.Entities;
using Microsoft.Extensions.Logging;

namespace InStock.Lib.Services
{
  public interface IPositionCalculator
  {
    Task SetCalculableProperties(DateTime asOfUtc, ICalculablePosition target);

    bool IsLongPosition(decimal daysHeld);

    decimal CostBasis(decimal price, decimal shares);

    decimal Gain(decimal currentValue, decimal costBasis);

    decimal GainPercentage(decimal totalGain, decimal costBasis);

    decimal TheoreticalValue(decimal gainPercentage, decimal costBasis);

    decimal TheoreticalPrice(decimal theoreticalValue, decimal shares);
  }

  public class PositionCalculator
    : IPositionCalculator
  {
    //I don't like injecting a Service into a Service, but this is an edge case
    private readonly IQuoteService _quoteService;
    private readonly ILogger<PositionCalculator> _logger;

    private const int DaysInOneYear = 365;

    public PositionCalculator(
      ILogger<PositionCalculator> logger,
      IQuoteService quoteService)
    {
      _logger = logger;
      _quoteService = quoteService;
    }

    public async Task SetCalculableProperties(DateTime asOfUtc, ICalculablePosition target)
    {
      QuoteEntity quote;

      try
      {
        //The YahooApi can throw stupid unexpected exceptions sometimes
        quote = await _quoteService.Add(target.StockId, target.Symbol);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "YahooApi Failure");

        quote = new QuoteEntity();
      }

      target.CurrentPrice = Convert.ToDecimal(quote.Price);
      target.CurrentValue = CostBasis(target.CurrentPrice, target.Shares);
      target.DaysHeld = DaysHeld(asOfUtc, target.AcquiredOnUtc);
      target.TotalGain = Gain(target.CurrentValue, target.CostBasis);
      target.TotalGainPercentage = GainPercentage(target.TotalGain, target.CostBasis);
      target.GainRate = SafeDivide(target.TotalGain, target.DaysHeld);
    }

    //currentValue - costBasis = gain
    public decimal Gain(decimal currentValue, decimal costBasis) => currentValue - costBasis;

    //(currentValue - costBasis) / costBasis = gainPercentage
    public decimal GainPercentage(decimal totalGain, decimal costBasis)
      => SafeDivide(totalGain, costBasis);

    //(currentValue - costBasis) / costBasis = gainPercentage -> (gainPercentage * costBasis) + costBasis = theoreticalValue
    public decimal TheoreticalValue(decimal gainPercentage, decimal costBasis)
      => (gainPercentage * costBasis) + costBasis;

    public decimal TheoreticalPrice(decimal theoreticalValue, decimal shares) => SafeDivide(theoreticalValue, shares);

    public decimal CostBasis(decimal price, decimal shares) => price * shares;

    public decimal DaysHeld(DateTime asOfUtc, DateTime acquisitionDate) => Convert.ToDecimal((asOfUtc - acquisitionDate).TotalDays);

    public bool IsLongPosition(decimal daysHeld) => daysHeld >= DaysInOneYear;

    private static decimal SafeDivide(decimal numerator, decimal divisor)
    {
      if (divisor == 0) return 0;

      return numerator / divisor;
    }
  }
}
