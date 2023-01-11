using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public interface IStockService
    {
        StockEntity GetStock(int id);

        StockEntity GetStock(string symbol);

        Task<StockEntity> Add(string symbol);
    }
}