using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class EarningsV1PatchModel
    {
        public EarningsV1PatchModel()
        {
            
        }

        public EarningsV1PatchModel(EarningsEntity entity)
        {
            EarningsId = entity.EarningsId;
            Date = entity.Date;
            Order = entity.Order;
        }

        public int EarningsId { get; set; }

        public DateTime Date { get; set; }
        
        public int Order { get; set; }
    }
}
