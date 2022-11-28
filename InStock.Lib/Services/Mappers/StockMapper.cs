using InStock.Lib.Entities;
using InStock.Lib.Models;

namespace InStock.Lib.Services.Mappers
{
    public class StockMapper
    {
        public StockEntity ToEntity(StockModel model)
        {
            var entity = new StockEntity();
            entity.StockId = model.StockId;
            entity.Symbol = model.Symbol;
            entity.Name = model.Name;
            entity.CreateOnUtc = model.CreateOnUtc;
            entity.Notes = model.Notes;

            return entity;
        }

        public StockModel ToModel(StockEntity entity)
        {
            var model = new StockModel();
            model.StockId = entity.StockId;
            model.Symbol = entity.Symbol;
            model.Name = entity.Name;
            model.CreateOnUtc = entity.CreateOnUtc;
            model.Notes = entity.Notes;

            return model;
        }

        public StockEntity ToEntity(IStock target)
        {
            var entity = new StockEntity();
            entity.StockId = target.StockId;
            entity.Symbol = target.Symbol;
            entity.Name = target.Name;
            entity.CreateOnUtc = target.CreateOnUtc;
            entity.Notes = target.Notes;

            return entity;
        }

        public StockModel ToModel(IStock target)
        {
            var model = new StockModel();
            model.StockId = target.StockId;
            model.Symbol = target.Symbol;
            model.Name = target.Name;
            model.CreateOnUtc = target.CreateOnUtc;
            model.Notes = target.Notes;

            return model;
        }
    }
}
