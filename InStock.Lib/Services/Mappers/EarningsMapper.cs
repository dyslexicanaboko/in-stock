using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services.Mappers
{
    public class EarningsMapper
        : MapperBase, IEarningsMapper
    {
        public EarningsEntity? ToEntity(EarningsModel? model)
        {
            if (model == null) return null;

            var entity = new EarningsEntity(model);

            return entity;
        }

        public EarningsModel? ToModel(EarningsEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new EarningsModel(entity);

            return model;
        }

        public EarningsV1PatchModel? ToPatchModel(EarningsEntity? entity)
        {
            if (entity == null) return null;

            var model = new EarningsV1PatchModel(entity);

            return model;
        }

        public EarningsEntity? ToEntity(IEarnings? target)
        {
            if (target == null) return null;
         
            var entity = new EarningsEntity(target);

            return entity;
        }

        public EarningsModel? ToModel(IEarnings? target)
        {
            if (target == null) return null;
            
            var model = new EarningsModel(target);

            return model;
        }

        public EarningsEntity? ToEntity(EarningsV1CreateModel? model)
        {
            if (model == null) return null;
            
            var entity = new EarningsEntity(model);

            return entity;
        }

        public EarningsEntity? ToEntity(int stockId, EarningsV1PatchModel? model)
        {
            if (model == null) return null;

            var entity = new EarningsEntity(stockId, model);

            return entity;
        }

        public EarningsV1CreatedModel? ToCreatedModel(EarningsEntity? entity)
        {
            if (entity == null) return null;

            var model = new EarningsV1CreatedModel(entity);

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
        
        public IList<EarningsModel> ToModel(IList<EarningsEntity>? target) => ToList(target, ToModel);

        public IList<EarningsEntity> ToEntity(IList<EarningsV1CreateModel>? target) => ToList(target, ToEntity);
    }
}
