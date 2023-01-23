using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public class TradeMapper
        : MapperBase, IMapper<ITrade, TradeEntity, TradeModel>, ITradeMapper
    {
        public TradeEntity? ToEntity(TradeModel? model)
        {
            if (model == null) return null;
            
            var entity = new TradeEntity();
            entity.TradeId = model.TradeId;
            entity.UserId = model.UserId;
            entity.StockId = model.StockId;
            entity.Type = model.Type;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.Confirmation = model.Confirmation;

            return entity;
        }

        public TradeModel? ToModel(TradeEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new TradeModel();
            model.TradeId = entity.TradeId;
            model.UserId = entity.UserId;
            model.StockId = entity.StockId;
            model.Type = entity.Type;
            model.Price = entity.Price;
            model.Quantity = entity.Quantity;
            model.StartDate = entity.StartDate;
            model.EndDate = entity.EndDate;
            model.Confirmation = entity.Confirmation;

            return model;
        }

        public TradeEntity? ToEntity(ITrade? target)
        {
            if (target == null) return null;
            
            var entity = new TradeEntity();
            entity.TradeId = target.TradeId;
            entity.UserId = target.UserId;
            entity.StockId = target.StockId;
            entity.Type = target.Type;
            entity.Price = target.Price;
            entity.Quantity = target.Quantity;
            entity.StartDate = target.StartDate;
            entity.EndDate = target.EndDate;
            entity.Confirmation = target.Confirmation;

            return entity;
        }

        public TradeModel? ToModel(ITrade? target)
        {
            if (target == null) return null;
            
            var model = new TradeModel();
            model.TradeId = target.TradeId;
            model.UserId = target.UserId;
            model.StockId = target.StockId;
            model.Type = target.Type;
            model.Price = target.Price;
            model.Quantity = target.Quantity;
            model.StartDate = target.StartDate;
            model.EndDate = target.EndDate;
            model.Confirmation = target.Confirmation;

            return model;
        }

        public TradeEntity? ToEntity(TradeV1CreateModel? model)
        {
            if (model == null) return null;

            var entity = new TradeEntity();
            entity.StockId = model.StockId;
            entity.Type = model.Type;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;
            entity.Confirmation = model.Confirmation;

            return entity;
        }

        public IList<TradeModel> ToModel(IList<TradeEntity>? target) => ToList(target, ToModel);
    }
}
