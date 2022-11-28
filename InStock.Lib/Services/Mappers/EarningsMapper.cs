using InStock.Lib.Entities;
using InStock.Lib.Models;

namespace InStock.Lib.Services.Mappers
{
    public class EarningsMapper
    {
        public EarningsEntity ToEntity(EarningsModel model)
        {
            var entity = new EarningsEntity();
            entity.EarningsId = model.EarningsId;
            entity.StockId = model.StockId;
            entity.Date = model.Date;
            entity.Order = model.Order;
            entity.CreateOnUtc = model.CreateOnUtc;

            return entity;
        }

        public EarningsModel ToModel(EarningsEntity entity)
        {
            var model = new EarningsModel();
            model.EarningsId = entity.EarningsId;
            model.StockId = entity.StockId;
            model.Date = entity.Date;
            model.Order = entity.Order;
            model.CreateOnUtc = entity.CreateOnUtc;

            return model;
        }

        public EarningsEntity ToEntity(IEarnings target)
        {
            var entity = new EarningsEntity();
            entity.EarningsId = target.EarningsId;
            entity.StockId = target.StockId;
            entity.Date = target.Date;
            entity.Order = target.Order;
            entity.CreateOnUtc = target.CreateOnUtc;

            return entity;
        }

        public EarningsModel ToModel(IEarnings target)
        {
            var model = new EarningsModel();
            model.EarningsId = target.EarningsId;
            model.StockId = target.StockId;
            model.Date = target.Date;
            model.Order = target.Order;
            model.CreateOnUtc = target.CreateOnUtc;

            return model;
        }
    }
}
