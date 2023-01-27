using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
    public interface ITradeRepository
        : IRepository<TradeEntity>
    {
        IEnumerable<TradeEntity> Select(int userId, string symbol);

        void Delete(int tradeId);

        void Delete(int userId, string symbol);
    }
}
