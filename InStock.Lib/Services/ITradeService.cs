using InStock.Lib.Entities;

namespace InStock.Lib.Services;

public interface ITradeService
{
    TradeEntity? GetTrade(int id);
    IList<TradeEntity> GetTrade(int userId, string symbol);
    TradeEntity Add(TradeEntity? trade);
    void Delete(int tradeId);
    void Delete(int userId, string symbol);
}