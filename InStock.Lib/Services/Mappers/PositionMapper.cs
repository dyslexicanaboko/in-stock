using InStock.Lib.Entities;
using InStock.Lib.Entities.Composites;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services.Mappers
{
    public class PositionMapper
        : MapperBase, IPositionMapper
    {
        public PositionEntity? ToEntity(PositionModel? model)
        {
            if (model == null) return null;

            var entity = new PositionEntity(model);

            return entity;
        }

        public PositionModel? ToModel(PositionEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new PositionModel(entity);

            return model;
        }

        public PositionV1GetCalculatedModel? ToModel(PositionComposite? composite)
        {
          if (composite == null) return null;

          var model = new PositionV1GetCalculatedModel(composite);

          return model;
        }

    public PositionV1CreateModel? ToCreateModel(PositionEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new PositionV1CreateModel(entity);

            return model;
        }

        public PositionV1FailedCreateModel? ToFailedCreateModel(AddPositionResult? result)
        {
            if (result == null) return null;

            var model = new PositionV1FailedCreateModel(result.Position);
            model.FailureCode = 100; //This needs to be set correctly
            model.FailureReason = result.GetErrorMessage();

            return model;
        }

        public PositionEntity? ToEntity(IPosition? target)
        {
            if (target == null) return null;
            
            var entity = new PositionEntity(target);

            return entity;
        }

        public PositionModel? ToModel(IPosition? target)
        {
            if (target == null) return null;
            
            var model = new PositionModel(target);

            return model;
        }

        public PositionEntity? ToEntity(int userId, PositionV1CreateModel? model)
        {
            if (model == null) return null;

            var entity = new PositionEntity(userId, model);

            return entity;
        }

        public PositionV1CreateMultipleModel ToModel(IList<AddPositionResult> results)
        {
            var success = results
                .Where(x => x.IsSuccessful)
                .Select(x => ToModel(x.Position))
                .ToList();

            var failure = results
                .Where(x => !x.IsSuccessful)
                .Select(ToFailedCreateModel)
                .ToList();

            var m = new PositionV1CreateMultipleModel(success, failure);

            return m;
        }

        public PositionV1PatchModel? ToPatchModel(PositionEntity? entity)
        {
            if (entity == null) return null;

            var model = new PositionV1PatchModel(entity);

            return model;
        }

        public PositionEntity? ToEntity(int positionId, int stockId, PositionV1PatchModel? model)
        {
            if (model == null) return null;

            var entity = new PositionEntity(positionId, stockId, model);

            return entity;
        }

        public IList<PositionModel> ToModel(IList<PositionEntity>? target) => ToList(target, ToModel);
        
        public IList<PositionV1GetCalculatedModel> ToCalculatedModel(IList<PositionComposite>? target) => ToList(target, ToModel);
        
        public IList<PositionEntity> ToEntity(int userId, IList<PositionV1CreateModel>? target) => ToList(userId, target, ToEntity);
    }
}
