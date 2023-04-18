namespace InStock.Lib.Entities
{
    public interface ITrade
    {
        int TradeId { get; set; }

        int UserId { get; set; }

        int StockId { get; set; }

        int TradeTypeId { get; set; }

        decimal Price { get; set; }

        decimal Quantity { get; set; }

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        string? Confirmation { get; set; }
    }
}
