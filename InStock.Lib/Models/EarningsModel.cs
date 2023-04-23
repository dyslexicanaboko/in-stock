using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class EarningsModel : IEarnings
    {
        public EarningsModel(IEarnings model)
        {
            EarningsId = model.EarningsId;
            StockId = model.StockId;
            Date = model.Date;
            Order = model.Order;
        }

        public EarningsModel(EarningsEntity entity)
        {
            EarningsId = entity.EarningsId;
            StockId = entity.StockId;
            Date = entity.Date;
            Order = entity.Order;
            CreateOnUtc = entity.CreateOnUtc;
        }

        public int EarningsId { get; }

        public int StockId { get; }

        public DateTime Date { get; }

        public int Order { get; }

        public DateTime CreateOnUtc { get; }
    }
}
