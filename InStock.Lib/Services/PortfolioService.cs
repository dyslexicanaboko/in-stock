using InStock.Lib.DataAccess;
using InStock.Lib.Entities.Composites;

namespace InStock.Lib.Services
{
  public class PortfolioService
    : IPortfolioService
  {
    private readonly IPortfolioRepository _repoPortfolio;
    private readonly IDateTimeService _dateTimeService;
    private readonly IPositionCalculator _calculator;

    public PortfolioService(
      IPortfolioRepository repoPortfolio,
      IDateTimeService dateTimeService,
      IPositionCalculator calculator)
    {
      _repoPortfolio = repoPortfolio;
      _dateTimeService = dateTimeService;
      _calculator = calculator;
    }

    public async Task<IList<PortfolioComposite>> GetPortfolio(int userId)
    {
      var dtm = _dateTimeService.UtcNow;

      var lst = _repoPortfolio.Using(x => x.Select(userId)).ToList();

      //Each stock held requires an updated quote when the Portfolio is requested
      //Cache is being utilized internally to guard against spamming the YahooApi and the DB
      foreach (var p in lst)
      {
        await _calculator.SetCalculableProperties(dtm, p);
      }

      return lst;
    }
  }
}
