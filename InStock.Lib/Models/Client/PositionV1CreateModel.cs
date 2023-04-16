using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class PositionV1CreateModel
    {
        public PositionV1CreateModel()
        {

        }

        public PositionV1CreateModel(IPosition entity)
        {
            StockId = entity.StockId;
            DateOpened = entity.DateOpened;
            DateClosed = entity.DateClosed;
            Price = entity.Price;
            Quantity = entity.Quantity;
        }

        public int StockId { get; set; }

        public DateTime DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
