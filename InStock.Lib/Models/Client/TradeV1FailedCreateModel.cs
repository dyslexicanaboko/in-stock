using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class TradeV1FailedCreateModel
        : TradeV1CreateModel
    {
        public TradeV1FailedCreateModel(TradeEntity entity)
            : base(entity)
        {
            
        }

        public int FailureCode { get; set; }
        
        public string FailureReason { get; set; }
    }
}
