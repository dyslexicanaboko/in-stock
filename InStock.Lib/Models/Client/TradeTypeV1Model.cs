using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class TradeTypeV1Model
    {
        private readonly TradeType _tradeType;

        public TradeTypeV1Model(int tradeTypeId)
        {
            _tradeType = (TradeType)tradeTypeId;
        }

        public TradeTypeV1Model(TradeType tradeType)
        {
            _tradeType = tradeType;
        }

        public int TradeTypeId => Convert.ToInt32(_tradeType);

        public string Description => Convert.ToString(_tradeType)!;
    }
}
