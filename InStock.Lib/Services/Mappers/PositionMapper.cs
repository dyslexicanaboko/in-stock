using InStock.Lib.Entities;
using InStock.Lib.Models;

namespace InStock.Lib.Services.Mappers
{
    public class PositionMapper
    {
        public PositionEntity ToEntity(PositionModel model)
        {
            var entity = new PositionEntity();
            entity.PositionId = model.PositionId;
            entity.UserId = model.UserId;
            entity.StockId = model.StockId;
            entity.DateOpened = model.DateOpened;
            entity.DateClosed = model.DateClosed;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;
            entity.CreateOnUtc = model.CreateOnUtc;

            return entity;
        }

        public PositionModel ToModel(PositionEntity entity)
        {
            var model = new PositionModel();
            model.PositionId = entity.PositionId;
            model.UserId = entity.UserId;
            model.StockId = entity.StockId;
            model.DateOpened = entity.DateOpened;
            model.DateClosed = entity.DateClosed;
            model.Price = entity.Price;
            model.Quantity = entity.Quantity;
            model.CreateOnUtc = entity.CreateOnUtc;

            return model;
        }

        public PositionEntity ToEntity(IPosition target)
        {
            var entity = new PositionEntity();
            entity.PositionId = target.PositionId;
            entity.UserId = target.UserId;
            entity.StockId = target.StockId;
            entity.DateOpened = target.DateOpened;
            entity.DateClosed = target.DateClosed;
            entity.Price = target.Price;
            entity.Quantity = target.Quantity;
            entity.CreateOnUtc = target.CreateOnUtc;

            return entity;
        }

        public PositionModel ToModel(IPosition target)
        {
            var model = new PositionModel();
            model.PositionId = target.PositionId;
            model.UserId = target.UserId;
            model.StockId = target.StockId;
            model.DateOpened = target.DateOpened;
            model.DateClosed = target.DateClosed;
            model.Price = target.Price;
            model.Quantity = target.Quantity;
            model.CreateOnUtc = target.CreateOnUtc;

            return model;
        }
    }
}
