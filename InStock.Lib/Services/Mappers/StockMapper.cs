using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public class StockMapper
        : IStockMapper
    {
        public StockEntity? ToEntity(StockModel? model)
        {
            if (model == null) return null;
            
            var entity = new StockEntity(model);
            
            return entity;
        }

        public StockModel? ToModel(StockEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new StockModel(entity);
            
            return model;
        }

        public StockEntity? ToEntity(IStock? target)
        {
            if (target == null) return null;
            
            var entity = new StockEntity(target);
            
            return entity;
        }

        public StockModel? ToModel(IStock? target)
        {
            if (target == null) return null;
            
            var model = new StockModel(target);

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
