using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services.Mappers
{
    public class EarningsMapper
        : MapperBase, IMapper<IEarnings, EarningsEntity, EarningsModel>, IEarningsMapper
    {
        public EarningsEntity? ToEntity(EarningsModel? model)
        {
            if (model == null) return null;

            var entity = new EarningsEntity();
            entity.EarningsId = model.EarningsId;
            entity.StockId = model.StockId;
            entity.Date = model.Date;
            entity.Order = model.Order;
            entity.CreateOnUtc = model.CreateOnUtc;

            return entity;
        }

        public EarningsModel? ToModel(EarningsEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new EarningsModel();
            model.EarningsId = entity.EarningsId;
            model.StockId = entity.StockId;
            model.Date = entity.Date;
            model.Order = entity.Order;
            model.CreateOnUtc = entity.CreateOnUtc;

            return model;
        }

        public EarningsEntity? ToEntity(IEarnings? target)
        {
            if (target == null) return null;
         
            var entity = new EarningsEntity();
            entity.EarningsId = target.EarningsId;
            entity.StockId = target.StockId;
            entity.Date = target.Date;
            entity.Order = target.Order;

            return entity;
        }

        public EarningsModel? ToModel(IEarnings? target)
        {
            if (target == null) return null;
            
            var model = new EarningsModel();
            model.EarningsId = target.EarningsId;
            model.StockId = target.StockId;
            model.Date = target.Date;
            model.Order = target.Order;

            return model;
        }

        public IList<EarningsModel> ToModel(IList<EarningsEntity>? target) => ToList(target, ToModel);

        public EarningsEntity? ToEntity(EarningsV1CreateModel? model)
        {
            if (model == null) return null;
            
            var entity = new EarningsEntity();
            entity.StockId = model.StockId;
            entity.Date = model.Date;
            entity.Order = model.Order;

            return entity;
        }

        public EarningsV1CreatedModel? ToCreatedModel(EarningsEntity? entity)
        {
            if (entity == null) return null;

            var model = new EarningsV1CreatedModel();
            model.EarningsId = entity.EarningsId;
            model.StockId = entity.StockId;
            model.Date = entity.Date;
            model.Order = entity.Order;

            return model;
        }

        public EarningsV1FailedCreateModel? ToFailedCreateModel(AddEarningsResult? result)
        {
            if (result == null) return null;

            var model = new EarningsV1FailedCreateModel(result.Earnings);
            model.FailureCode = 100; //This needs to be set correctly
            model.FailureReason = result.GetErrorMessage();

            return model;
        }

        public IList<EarningsEntity> ToEntity(IList<EarningsV1CreateModel>? target) => ToList(target, ToEntity);
    }
}
