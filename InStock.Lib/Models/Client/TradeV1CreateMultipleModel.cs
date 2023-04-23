namespace InStock.Lib.Models.Client
{
    public class TradeV1CreateMultipleModel
    {
        public TradeV1CreateMultipleModel(IList<TradeModel> success, IList<TradeV1FailedCreateModel> failure)
        {
            Success = success;

            Failure = failure;
        }

        public IList<TradeModel> Success { get; set; }
        
        public IList<TradeV1FailedCreateModel> Failure { get; set; }
    }
}
