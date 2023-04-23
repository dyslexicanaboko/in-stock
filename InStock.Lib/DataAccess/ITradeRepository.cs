using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
    public interface ITradeRepository
        : IRepository<TradeEntity>
    {
        IEnumerable<TradeEntity> Select(int userId, string symbol);
        
        IEnumerable<TradeEntity> SelectAll(int stockId, int? exceptTradeId = null);

        void Delete(int tradeId);

        void Delete(int userId, string symbol);
    }
}
