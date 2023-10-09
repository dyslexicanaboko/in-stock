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
            DateOpenedUtc = entity.DateOpenedUtc;
            DateClosedUtc = entity.DateClosedUtc;
            Price = entity.Price;
            Quantity = entity.Quantity;
        }

        public PositionModel(IPosition target)
        {
            PositionId = target.PositionId;
            UserId = target.UserId;
            StockId = target.StockId;
            DateOpenedUtc = target.DateOpenedUtc;
            DateClosedUtc = target.DateClosedUtc;
            Price = target.Price;
            Quantity = target.Quantity;
        }

        public int PositionId { get; }

        public int UserId { get; }

        public int StockId { get; }

        public DateTime DateOpenedUtc { get; }

        public DateTime? DateClosedUtc { get; }

        public decimal Price { get; }

        public decimal Quantity { get; }
    }
}
