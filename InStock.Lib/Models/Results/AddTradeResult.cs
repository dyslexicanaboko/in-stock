using InStock.Lib.Entities;

namespace InStock.Lib.Models.Results
{
    public class AddTradeResult
        : ResultBase
    {
        public AddTradeResult(TradeEntity trade)
        {
            Trade = trade;
        }

        public TradeEntity Trade { get; set; }
    }
}
