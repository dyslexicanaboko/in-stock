using InStock.Lib.Models;

namespace InStock.Lib.Entities
{

    public class QuoteEntity : IQuote
    {
        public QuoteEntity()
        {
            
        }

        public QuoteEntity(QuoteModel model)
        {
            QuoteId = model.QuoteId;
            StockId = model.StockId;
            Date = model.Date;
            Price = model.Price;
            Volume = model.Volume;
            CreatedOnUtc = model.CreateOnUtc;
        }

        public QuoteEntity(IQuote target)
        {
            QuoteId = target.QuoteId;
            StockId = target.StockId;
            Date = target.Date;
            Price = target.Price;
            Volume = target.Volume;
        }

        public int QuoteId { get; set; }

        public int StockId { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }

        public long Volume { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}
