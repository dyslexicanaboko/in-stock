using InStock.Lib.Entities;

namespace InStock.Lib.Services.ApiClient
{
    public class StockQuoteModel
        : IQuoteMeta
    {
        public StockQuoteModel(DateTime date, string symbol, string name, double price, long volume)
        {
            Date = date;
            Symbol = symbol;
            Name = name;
            Price = price;
            Volume = volume;
        }

        public DateTime Date { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public long Volume { get; set; }
    }
}
