using InStock.Lib.Entities.Composites;

namespace InStock.Lib.DataAccess
{
  public interface IPortfolioRepository : IRepository
  {
    IEnumerable<PortfolioComposite> Select(int userId);
  }
}
