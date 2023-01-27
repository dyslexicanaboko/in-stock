namespace InStock.Lib.Models.Client
{
    public class QuoteV1CreatedModel
    {
        public int QuoteId { get; set; }

        public int StockId { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }

        public long Volume { get; set; }
    }
}
