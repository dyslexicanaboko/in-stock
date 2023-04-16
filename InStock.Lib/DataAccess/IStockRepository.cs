using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
	public interface IStockRepository
		: IRepository<StockEntity>
	{
		StockEntity? Select(string symbol);

		StockEntity? SelectByEarningsId(int earningsId);

		IEnumerable<StockEntity> Select(IList<int> stockId);

		void UpdateNotes(int stockId, string? notes);
	}
}
