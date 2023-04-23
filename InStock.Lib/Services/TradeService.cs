using CommunityToolkit.Diagnostics;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _repoTrade;
        private readonly IStockRepository _repoStock;

        public TradeService(
            ITradeRepository repoTrade,
            IStockRepository repoStock)
        {
            _repoTrade = repoTrade;

            _repoStock = repoStock;
        }

        public TradeEntity? GetTrade(int id)
        {
            var dbEntity = _repoTrade.Using(x => x.Select(id));

            return dbEntity;
        }

        public IList<TradeEntity> GetTrade(int userId, string symbol)
        {
            var lst = _repoTrade
                .Using(x => x.Select(userId, symbol))
                .ToList();

            return lst;
        }

        public IList<AddTradeResult> Add(TradeEntity trade)
            => Add(new List<TradeEntity> { trade });

        public IList<AddTradeResult> Add(IList<TradeEntity>? trades)
        {
            Guard.IsNotNull(trades);

            //Get a distinct list of Stock Ids as there is no guarantee they are the same
            var stockIds = trades.Select(x => x.StockId).Distinct().ToList();

            //Get all stocks to prove they exist and order them by Stock Id so that the trades
            //are processed in stock order (grouped by stockId)
            var stocks = _repoStock
                .Using(x => x.Select(stockIds))
                .OrderBy(x => x.StockId)
                .ToList();

            var tradesSorted = trades.OrderBy(x => x.StockId).ToList();

            var results = new List<AddTradeResult>(trades.Count);

            foreach (var p in tradesSorted)
            {
                var r = new AddTradeResult(p);

                try
                {
                    var s = stocks.SingleOrDefault(x => x.StockId == p.StockId);

                    //Stock must exist before attempting to make trades with it
                    if (s == null)
                        throw new StockNotFoundException(p.StockId);

                    r.Trade = Add(s.Symbol, p);

                    r.Success();
                }
                catch (Exception ex)
                {
                    r.Failure(ex);
                }

                results.Add(r);
            }
            return results;
        }

        private TradeEntity Add(string symbol, TradeEntity? trade)
        {
            Guard.IsNotNull(trade);
            Guard.IsGreaterThan(trade.UserId, 0, nameof(trade.UserId));
            Guard.IsGreaterThan(trade.StockId, 0, nameof(trade.StockId));
            Guard.IsGreaterThan(trade.Price, 0, nameof(trade.Price));
            Guard.IsGreaterThan(trade.Quantity, 0, nameof(trade.Quantity));
            Guard.IsGreaterThan(trade.ExecutionDate, DateTime.MinValue, nameof(trade.ExecutionDate));

            using (_repoTrade)
            {
                //For now, not going to store existing trades in memory on the off chance trades
                //are being added via different threads somehow for the same user. Low chance, but possible.
                var trades = _repoTrade.Select(trade.UserId, symbol).ToList();

                //To avoid breaking unique constraints check if existing trades do not conflict
                if (trades.Any(x => x == trade)) throw new TradeExistsAlreadyException(symbol, trade);

                //If this is a unique trade then insert it
                trade.TradeId = _repoTrade.Insert(trade);
            }

            return trade;
        }

        //Trade only affects reporting, so if it is deleted, then it just needs to be re-added.
        public void Delete(int tradeId)
        {
            _repoTrade.Using(x => x.Delete(tradeId));
        }

        public void Delete(int userId, string symbol)
        {
            Guard.IsGreaterThan(userId, 0, nameof(userId));
            Guard.IsNotNullOrWhiteSpace(symbol, nameof(symbol));

            if (_repoStock.Using(x => x.Select(symbol)) is null) throw new SymbolNotFoundException(symbol);

            _repoTrade.Using(x => x.Delete(userId, symbol));
        }
    }
}
