using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class PositionV1CreateModel
    {
        public PositionV1CreateModel()
        {
            //Required for controller
        }

        public PositionV1CreateModel(IPosition entity)
        {
            StockId = entity.StockId;
            DateOpenedUtc = entity.DateOpenedUtc;
            DateClosedUtc = entity.DateClosedUtc;
            Price = entity.Price;
            Quantity = entity.Quantity;
        }

        public int StockId { get; set; }

        public DateTime DateOpenedUtc { get; set; }

        public DateTime? DateClosedUtc { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
