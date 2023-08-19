namespace InStock.Lib.Entities
{
    public interface IPosition
    {
      public int PositionId { get; }

        int UserId { get; }

        int StockId { get; }

        public DateTime DateOpened { get; }

        public DateTime? DateClosed { get; }

        public decimal Price { get; }

        decimal Quantity { get; }
    }
}
