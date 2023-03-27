using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
	public interface IStockRepository
		: IRepository<StockEntity>
	{
		StockEntity? Select(string symbol);

		IEnumerable<StockEntity> Select(IList<int> stockId);

		void UpdateNotes(int stockId, string? notes);
	}
}
