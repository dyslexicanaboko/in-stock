using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class EarningsV1CreatedModel
    {
        public EarningsV1CreatedModel(EarningsEntity entity)
        {
            EarningsId = entity.EarningsId;
            StockId = entity.StockId;
            Date = entity.Date;
            Order = entity.Order;
        }

        public int EarningsId { get; }

        public int StockId { get; }

        public DateTime Date { get; }

        public int Order { get; }
    }
}
