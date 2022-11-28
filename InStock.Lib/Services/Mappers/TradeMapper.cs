using InStock.Lib.Entities;
using InStock.Lib.Models;

namespace InStock.Lib.Services.Mappers
{
    public class TradeMapper
    {
        public TradeEntity ToEntity(TradeModel model)
        {
            var entity = new TradeEntity();
            entity.TradeId = model.TradeId;
            entity.UserId = model.UserId;
            entity.StockId = model.StockId;
            entity.Type = model.Type;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.CreateOnUtc = model.CreateOnUtc;
            entity.Confirmation = model.Confirmation;

            return entity;
        }

        public TradeModel ToModel(TradeEntity entity)
        {
            var model = new TradeModel();
            model.TradeId = entity.TradeId;
            model.UserId = entity.UserId;
            model.StockId = entity.StockId;
            model.Type = entity.Type;
            model.Price = entity.Price;
            model.Quantity = entity.Quantity;
            model.StartDate = entity.StartDate;
            model.EndDate = entity.EndDate;
            model.CreateOnUtc = entity.CreateOnUtc;
            model.Confirmation = entity.Confirmation;

            return model;
        }

        public TradeEntity ToEntity(ITrade target)
        {
            var entity = new TradeEntity();
            entity.TradeId = target.TradeId;
            entity.UserId = target.UserId;
            entity.StockId = target.StockId;
            entity.Type = target.Type;
            entity.Price = target.Price;
            entity.Quantity = target.Quantity;
            entity.StartDate = target.StartDate;
            entity.EndDate = target.EndDate;
            entity.CreateOnUtc = target.CreateOnUtc;
            entity.Confirmation = target.Confirmation;

            return entity;
        }

        public TradeModel ToModel(ITrade target)
        {
            var model = new TradeModel();
            model.TradeId = target.TradeId;
            model.UserId = target.UserId;
            model.StockId = target.StockId;
            model.Type = target.Type;
            model.Price = target.Price;
            model.Quantity = target.Quantity;
            model.StartDate = target.StartDate;
            model.EndDate = target.EndDate;
            model.CreateOnUtc = target.CreateOnUtc;
            model.Confirmation = target.Confirmation;

            return model;
        }
    }
}
