using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public class StockMapper
        : IMapper<IStock, StockEntity, StockModel>, IStockMapper
    {
        public StockEntity? ToEntity(StockModel? model)
        {
            if (model == null) return null;
            
            var entity = new StockEntity(model);
            entity.StockId = model.StockId;
            entity.CreateOnUtc = model.CreateOnUtc;
            entity.Notes = model.Notes;

            return entity;
        }

        public StockModel? ToModel(StockEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new StockModel(entity);
            model.StockId = entity.StockId;
            model.CreateOnUtc = entity.CreateOnUtc;
            model.Notes = entity.Notes;

            return model;
        }

        public StockEntity? ToEntity(IStock? target)
        {
            if (target == null) return null;
            
            var entity = new StockEntity(target);
            entity.StockId = target.StockId;
            entity.Notes = target.Notes;

            return entity;
        }

        public StockModel? ToModel(IStock? target)
        {
            if (target == null) return null;
            
            var model = new StockModel(target);
            model.StockId = target.StockId;
            model.Notes = target.Notes;

            return model;
        }

        public StockV1CreatedModel? ToCreatedModel(IStock? target)
        {
            if (target == null) return null;
            
            var model = new StockV1CreatedModel(target);

            return model;
        }
    }
}
