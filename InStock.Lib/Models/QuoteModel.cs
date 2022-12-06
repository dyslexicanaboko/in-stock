using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class QuoteModel : IQuote
    {
        public int QuoteId { get; set; }

        public int StockId { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public decimal Volume { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
