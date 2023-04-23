using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class PositionV1FailedCreateModel
        : PositionV1CreateModel
    {
        public PositionV1FailedCreateModel(IPosition entity)
            : base(entity)
        {
            
        }

        public int FailureCode { get; set; }
        
        public string FailureReason { get; set; }
    }
}
