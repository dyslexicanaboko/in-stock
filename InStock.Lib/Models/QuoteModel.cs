using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class QuoteModel : IQuote
    {
        public QuoteModel(QuoteEntity entity)
        {
            QuoteId = entity.QuoteId;
            StockId = entity.StockId;
            Date = entity.Date;
            Price = entity.Price;
            Volume = entity.Volume;
            CreateOnUtc = entity.CreateOnUtc;
        }

        public QuoteModel(IQuote target)
        {
            QuoteId = target.QuoteId;
            StockId = target.StockId;
            Date = target.Date;
            Price = target.Price;
            Volume = target.Volume;
        }

        public int QuoteId { get; }

        public int StockId { get; }

        public DateTime Date { get; }

        public double Price { get; }

        public long Volume { get; }

        public DateTime CreateOnUtc { get; }
    }
}
