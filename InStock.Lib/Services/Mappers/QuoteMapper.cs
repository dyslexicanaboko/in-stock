using InStock.Lib.Entities;
using InStock.Lib.Models;

namespace InStock.Lib.Services.Mappers
{
    public class QuoteMapper
        : IMapper<IQuote, QuoteEntity, QuoteModel>
    {
        public QuoteEntity ToEntity(QuoteModel model)
        {
            var entity = new QuoteEntity();
            entity.QuoteId = model.QuoteId;
            entity.StockId = model.StockId;
            entity.Date = model.Date;
            entity.Price = model.Price;
            entity.Volume = model.Volume;
            entity.CreateOnUtc = model.CreateOnUtc;

            return entity;
        }

        public QuoteModel ToModel(QuoteEntity entity)
        {
            var model = new QuoteModel();
            model.QuoteId = entity.QuoteId;
            model.StockId = entity.StockId;
            model.Date = entity.Date;
            model.Price = entity.Price;
            model.Volume = entity.Volume;
            model.CreateOnUtc = entity.CreateOnUtc;

            return model;
        }

        public QuoteEntity ToEntity(IQuote target)
        {
            var entity = new QuoteEntity();
            entity.QuoteId = target.QuoteId;
            entity.StockId = target.StockId;
            entity.Date = target.Date;
            entity.Price = target.Price;
            entity.Volume = target.Volume;
            entity.CreateOnUtc = target.CreateOnUtc;

            return entity;
        }

        public QuoteModel ToModel(IQuote target)
        {
            var model = new QuoteModel();
            model.QuoteId = target.QuoteId;
            model.StockId = target.StockId;
            model.Date = target.Date;
            model.Price = target.Price;
            model.Volume = target.Volume;
            model.CreateOnUtc = target.CreateOnUtc;

            return model;
        }
    }
}
