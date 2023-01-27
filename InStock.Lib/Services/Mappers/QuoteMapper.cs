using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public class QuoteMapper
        : IMapper<IQuote, QuoteEntity, QuoteModel>, IQuoteMapper
    {
        public QuoteEntity? ToEntity(QuoteModel? model)
        {
            if (model == null) return null;

            var entity = new QuoteEntity();
            entity.QuoteId = model.QuoteId;
            entity.StockId = model.StockId;
            entity.Date = model.Date;
            entity.Price = model.Price;
            entity.Volume = model.Volume;
            entity.CreateOnUtc = model.CreateOnUtc;

            return entity;
        }

        public QuoteModel? ToModel(QuoteEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new QuoteModel();
            model.QuoteId = entity.QuoteId;
            model.StockId = entity.StockId;
            model.Date = entity.Date;
            model.Price = entity.Price;
            model.Volume = entity.Volume;
            model.CreateOnUtc = entity.CreateOnUtc;

            return model;
        }

        public QuoteEntity? ToEntity(IQuote? target)
        {
            if (target == null) return null;
            
            var entity = new QuoteEntity();
            entity.QuoteId = target.QuoteId;
            entity.StockId = target.StockId;
            entity.Date = target.Date;
            entity.Price = target.Price;
            entity.Volume = target.Volume;

            return entity;
        }

        public QuoteModel? ToModel(IQuote? target)
        {
            if (target == null) return null;
            
            var model = new QuoteModel();
            model.QuoteId = target.QuoteId;
            model.StockId = target.StockId;
            model.Date = target.Date;
            model.Price = target.Price;
            model.Volume = target.Volume;

            return model;
        }

        public QuoteV1CreatedModel ToCreatedModel(IQuote target)
        {
            var model = new QuoteV1CreatedModel();
            model.QuoteId = target.QuoteId;
            model.StockId = target.StockId;
            model.Date = target.Date;
            model.Price = target.Price;
            model.Volume = target.Volume;

            return model;
        }
    }
}
