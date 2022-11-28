namespace InStock.Lib.Entities
{
    public interface IQuote
    {
        int QuoteId { get; set; }

        int StockId { get; set; }

        DateTime Date { get; set; }

        decimal Price { get; set; }

        decimal Volume { get; set; }

        DateTime CreateOnUtc { get; set; }
    }
}
