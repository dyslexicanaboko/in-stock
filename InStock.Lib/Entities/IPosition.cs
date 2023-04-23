namespace InStock.Lib.Entities
{
    public interface IPosition
    {
        int PositionId { get; }

        int UserId { get; }

        int StockId { get; }

        DateTime DateOpened { get; }

        DateTime? DateClosed { get; }

        decimal Price { get; }

        decimal Quantity { get; }
    }
}
