using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class PositionModel : IPosition
    {
        public int PositionId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }

        public DateTime DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
