using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class PositionModel : IPosition
    {
        public PositionModel(PositionEntity entity)
        {
            PositionId = entity.PositionId;
            UserId = entity.UserId;
            StockId = entity.StockId;
            DateOpened = entity.DateOpened;
            DateClosed = entity.DateClosed;
            Price = entity.Price;
            Quantity = entity.Quantity;
        }

        public PositionModel(IPosition target)
        {
            PositionId = target.PositionId;
            UserId = target.UserId;
            StockId = target.StockId;
            DateOpened = target.DateOpened;
            DateClosed = target.DateClosed;
            Price = target.Price;
            Quantity = target.Quantity;
        }

        public int PositionId { get; }

        public int UserId { get; }

        public int StockId { get; }

        public DateTime DateOpened { get; }

        public DateTime? DateClosed { get; }

        public decimal Price { get; }

        public decimal Quantity { get; }
    }
}
