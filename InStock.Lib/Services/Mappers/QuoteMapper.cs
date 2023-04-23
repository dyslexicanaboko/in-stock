using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public class QuoteMapper
        : IQuoteMapper
    {
        public QuoteEntity? ToEntity(QuoteModel? model)
        {
            if (model == null) return null;

            var entity = new QuoteEntity(model);

            return entity;
        }

        public QuoteModel? ToModel(QuoteEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new QuoteModel(entity);
            
            return model;
        }

        public QuoteEntity? ToEntity(IQuote? target)
        {
            if (target == null) return null;
            
            var entity = new QuoteEntity(target);
            
            return entity;
        }

        public QuoteModel? ToModel(IQuote? target)
        {
            if (target == null) return null;
            
            var model = new QuoteModel(target);

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
