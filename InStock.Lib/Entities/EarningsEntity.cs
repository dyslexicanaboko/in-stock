using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Entities
{
    public class EarningsEntity : IEarnings
    {
        public EarningsEntity()
        {
            
        }

        public EarningsEntity(IEarnings model)
        {
            EarningsId = model.EarningsId;
            StockId = model.StockId;
            Date = model.Date;
            Order = model.Order;
        }

        public EarningsEntity(EarningsModel model)
        {
            EarningsId = model.EarningsId;
            StockId = model.StockId;
            Date = model.Date;
            Order = model.Order;
            CreateOnUtc = model.CreateOnUtc;
        }

        public EarningsEntity(EarningsV1PatchModel model)
        {
            EarningsId = model.EarningsId;
            Date = model.Date;
            Order = model.Order;
        }

        public EarningsEntity(EarningsV1CreateModel model)
        {
            StockId = model.StockId;
            Date = model.Date;
            Order = model.Order;
        }

        public int EarningsId { get; set; }

        public int StockId { get; set; }

        public DateTime Date { get; set; }

        public int Order { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
