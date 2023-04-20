using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public class TradeMapper
        : MapperBase, ITradeMapper
    {
        public TradeEntity? ToEntity(TradeModel? model)
        {
            if (model == null) return null;
            
            var entity = new TradeEntity(model);

            return entity;
        }

        public TradeModel? ToModel(TradeEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new TradeModel(entity);

            return model;
        }

        public TradeEntity? ToEntity(ITrade? target)
        {
            if (target == null) return null;
            
            var entity = new TradeEntity(target);

            return entity;
        }

        public TradeModel? ToModel(ITrade? target)
        {
            if (target == null) return null;
            
            var model = new TradeModel(target);
            
            return model;
        }

        public TradeEntity? ToEntity(int userId, TradeV1CreateModel? model)
        {
            if (model == null) return null;

            var entity = new TradeEntity(userId, model);

            return entity;
        }

        public IList<TradeModel> ToModel(IList<TradeEntity>? target) => ToList(target, ToModel);
    }
}
