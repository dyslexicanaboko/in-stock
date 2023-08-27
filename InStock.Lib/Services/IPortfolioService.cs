using InStock.Lib.Entities.Composites;

namespace InStock.Lib.Services;

public interface IPortfolioService
{
  Task<IList<PortfolioComposite>> GetPortfolio(int userId);
}
