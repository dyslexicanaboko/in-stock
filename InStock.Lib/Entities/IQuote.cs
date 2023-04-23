namespace InStock.Lib.Entities
{
    public interface IQuote
        : IQuoteMeta
    {
        int QuoteId { get; }

        int StockId { get; }
    }
}
