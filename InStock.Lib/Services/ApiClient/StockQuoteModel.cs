using InStock.Lib.Entities;

namespace InStock.Lib.Services.ApiClient
{
    public class StockQuoteModel
        : IQuoteMeta
    {
        public DateTime Date { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public long Volume { get; set; }
    }
}
