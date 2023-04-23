using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class EarningsV1FailedCreateModel
        : EarningsV1CreateModel
    {
        public EarningsV1FailedCreateModel(IEarnings entity)
            : base(entity)
        {
            
        }

        public int FailureCode { get; set; }
        
        public string FailureReason { get; set; }
    }
}
