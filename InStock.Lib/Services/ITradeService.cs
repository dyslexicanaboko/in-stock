using InStock.Lib.Entities;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services;

public interface ITradeService
{
    TradeEntity? GetTrade(int id);
    
    IList<TradeEntity> GetTrade(int userId, string symbol);

    IList<AddTradeResult> Add(TradeEntity trade);
    
    IList<AddTradeResult> Add(IList<TradeEntity>? trade);

    void Delete(int tradeId);
    
    void Delete(int userId, string symbol);
}