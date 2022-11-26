using System;

namespace InStock.Lib.Entities
{
    public interface IPosition
    {
        int PositionId { get; set; }

        int UserId { get; set; }

        int StockId { get; set; }

        DateTime DateOpened { get; set; }

        DateTime? DateClosed { get; set; }

        decimal Price { get; set; }

        decimal Quantity { get; set; }

        DateTime CreateOnUtc { get; set; }
    }
}
