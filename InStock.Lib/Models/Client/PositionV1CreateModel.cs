namespace InStock.Lib.Models.Client
{
    public class PositionV1CreateModel
    {
        public PositionV1CreateModel()
        {

        }

        //public PositionV1CreateModel(int stockId, DateTime dateOpened, DateTime? dateClosed, decimal price, decimal quantity)
        //{
        //    StockId = stockId;

        //    DateOpened = dateOpened;

        //    DateClosed = dateClosed;

        //    Price = price;

        //    Quantity = quantity;
        //}

        public int StockId { get; set; }

        public DateTime DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
