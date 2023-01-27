namespace InStock.Lib.Entities
{
    public interface IQuote
        : IQuoteMeta
    {
        int QuoteId { get; set; }

        int StockId { get; set; }
    }
}
