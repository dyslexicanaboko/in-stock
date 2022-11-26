using System;

namespace InStock.Lib.Entities
{

    public class PositionEntity : IPosition
    {
        public int PositionId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }

        public DateTime DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
