namespace InStock.Lib.Models.Client
{
    public class PositionV1CreateMultipleModel
    {
        public PositionV1CreateMultipleModel(IList<PositionModel> success, IList<PositionV1FailedCreateModel> failure)
        {
            Success = success;

            Failure = failure;
        }

        public IList<PositionModel> Success { get; }
        
        public IList<PositionV1FailedCreateModel> Failure { get; }
    }
}
