namespace InStock.Lib.Models.Client
{
    public class EarningsV1CreateMultipleModel
    {
        public EarningsV1CreateMultipleModel(IList<EarningsModel> success, IList<EarningsV1FailedCreateModel> failure)
        {
            Success = success;

            Failure = failure;
        }

        public IList<EarningsModel> Success { get; set; }
        
        public IList<EarningsV1FailedCreateModel> Failure { get; set; }
    }
}
