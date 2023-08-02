using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Entities.Composites;
using Microsoft.Extensions.Logging;

namespace InStock.Lib.Services
{
  public class PortfolioService
    : IPortfolioService
  {
    private readonly IPortfolioRepository _repoPortfolio;
    //I don't like injecting a Service into a Service, but this is an edge case
    private readonly IQuoteService _quoteService;
    private readonly ILogger<PortfolioService> _logger;

    public PortfolioService(
      ILogger<PortfolioService> logger,
      IPortfolioRepository repoPortfolio,
      IQuoteService quoteService)
    {
      _logger = logger;
      _repoPortfolio = repoPortfolio;
      _quoteService = quoteService;
    }

    public async Task<IList<PortfolioComposite>> GetPortfolio(int userId)
    {
      var dtm = DateTime.UtcNow;

      var lst = _repoPortfolio.Using(x => x.Select(userId)).ToList();

      //Each stock held requires an updated quote when the Portfolio is requested
      //Cache is being utilized internally to guard against spamming the YahooApi and the DB
      foreach (var p in lst)
      {
        QuoteEntity quote;

        try
        {
          //The YahooApi can throw stupid unexpected exceptions sometimes
          quote = await _quoteService.Add(p.StockId, p.Symbol);
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, "YahooApi Failure");

          quote = new QuoteEntity();
        }

        p.CurrentPrice = Convert.ToDecimal(quote.Price);
        p.CurrentValue = p.CurrentPrice * p.Shares;
        p.DaysHeld = Convert.ToDecimal((dtm - p.OwnedAsOf).TotalDays);
        p.TotalGain = p.CurrentValue - p.CostBasis;
        p.TotalGainPercentage = 1M - SafeDivide(p.TotalGain, p.CostBasis);
        p.GainRate = SafeDivide(p.TotalGain, p.DaysHeld);
      }

      return lst;
    }

    private static decimal SafeDivide(decimal numerator, decimal divisor)
    {
      if (divisor == 0) return 0;

      return numerator / divisor;
    }
  }
}
