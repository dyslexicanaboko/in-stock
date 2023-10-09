namespace InStock.Lib.Entities
{
    public interface ITrade
    {
        int TradeId { get; }

        int UserId { get; }

        int StockId { get; }

        int TradeTypeId { get; }

        decimal Price { get; }

        decimal Quantity { get; }

        DateTime ExecutionDateUtc { get; }

        string? Confirmation { get; }
    }
}
