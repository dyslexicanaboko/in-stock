namespace InStock.Lib.Entities
{

    public class QuoteEntity : IQuote
    {
        public int QuoteId { get; set; }

        public int StockId { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }

        public long Volume { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
