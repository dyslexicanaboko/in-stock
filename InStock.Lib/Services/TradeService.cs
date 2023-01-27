using CommunityToolkit.Diagnostics;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _repoTrade;

        public TradeService(
            ITradeRepository repoTrade)
        {
            _repoTrade = repoTrade;
        }

        public TradeEntity? GetTrade(int id)
        {
            var dbEntity = _repoTrade.Using(x => x.Select(id));

            return dbEntity;
        }

        //TODO: Need to check for existence of the symbol?
        public IList<TradeEntity> GetTrade(int userId, string symbol)
        {
            var lst = _repoTrade
                .Using(x => x.Select(userId, symbol))
                .ToList();

            return lst;
        }

        //TODO: Prevent duplicate entries of trades
        //TODO: Enforce FK of StockId existence
        public TradeEntity Add(TradeEntity? trade)
        {
            Guard.IsNotNull(trade);
            
            trade.TradeId = _repoTrade.Using(x => x.Insert(trade));

            return trade;
        }

        //Trade only affects reporting, so if it is deleted, then it just needs to be re-added.
        public void Delete(int tradeId)
        {
            _repoTrade.Using(x => x.Delete(tradeId));
        }

        //TODO: Need to check for existence of the symbol?
        public void Delete(int userId, string symbol)
        {
            _repoTrade.Using(x => x.Delete(userId, symbol));
        }
    }
}
