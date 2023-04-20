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

        DateTime StartDate { get; }

        DateTime EndDate { get; }

        string? Confirmation { get; }
    }
}
