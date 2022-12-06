using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class StockQuoteModel
        : IQuoteMeta
    {
        public DateTime Date { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Volume { get; set; }
        
    }
}
