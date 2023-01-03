using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public interface IStockService
    {
        StockEntity GetStock(int id);

        Task<StockEntity> Add(string symbol);
    }
}