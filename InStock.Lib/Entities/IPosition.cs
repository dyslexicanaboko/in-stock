namespace InStock.Lib.Entities
{
    public interface IPosition
    {
      public int PositionId { get; }

        int UserId { get; }

        int StockId { get; }

        public DateTime DateOpenedUtc { get; }

        public DateTime? DateClosedUtc { get; }

        public decimal Price { get; }

        decimal Quantity { get; }
    }
}
