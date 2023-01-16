using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
	public interface IStockRepository
		: IRepository<StockEntity>
	{
		StockEntity? Select(string symbol);
	}
}
