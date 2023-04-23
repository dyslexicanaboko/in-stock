using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class EarningsV1CreateModel
    {
        public EarningsV1CreateModel()
        {
            //Required for controller
        }

        public EarningsV1CreateModel(IEarnings earnings)
        {
            StockId = earnings.StockId;
            
            Date = earnings.Date;
            
            Order = earnings.Order;
        }

        public int StockId { get; set; }

        public DateTime Date { get; set; }

        public int Order { get; set; }
    }
}
