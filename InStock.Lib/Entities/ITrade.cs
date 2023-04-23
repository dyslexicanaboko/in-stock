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

        DateTime ExecutionDate { get; }

        string? Confirmation { get; }
    }
}
