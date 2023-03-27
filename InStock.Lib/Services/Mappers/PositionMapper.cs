using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;
using static Dapper.SqlMapper;

namespace InStock.Lib.Services.Mappers
{
    public class PositionMapper
        : MapperBase, IMapper<IPosition, PositionEntity, PositionModel>, IPositionMapper
    {
        public PositionEntity? ToEntity(PositionModel? model)
        {
            if (model == null) return null;

            var entity = new PositionEntity();
            entity.PositionId = model.PositionId;
            entity.UserId = model.UserId;
            entity.StockId = model.StockId;
            entity.DateOpened = model.DateOpened;
            entity.DateClosed = model.DateClosed;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;

            return entity;
        }

        public PositionModel? ToModel(PositionEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new PositionModel();
            model.PositionId = entity.PositionId;
            model.UserId = entity.UserId;
            model.StockId = entity.StockId;
            model.DateOpened = entity.DateOpened;
            model.DateClosed = entity.DateClosed;
            model.Price = entity.Price;
            model.Quantity = entity.Quantity;

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
            
            var entity = new PositionEntity();
            entity.PositionId = target.PositionId;
            entity.UserId = target.UserId;
            entity.StockId = target.StockId;
            entity.DateOpened = target.DateOpened;
            entity.DateClosed = target.DateClosed;
            entity.Price = target.Price;
            entity.Quantity = target.Quantity;

            return entity;
        }

        public PositionModel? ToModel(IPosition? target)
        {
            if (target == null) return null;
            
            var model = new PositionModel();
            model.PositionId = target.PositionId;
            model.UserId = target.UserId;
            model.StockId = target.StockId;
            model.DateOpened = target.DateOpened;
            model.DateClosed = target.DateClosed;
            model.Price = target.Price;
            model.Quantity = target.Quantity;

            return model;
        }

        public PositionEntity? ToEntity(PositionV1CreateModel? model)
        {
            if (model == null) return null;

            var entity = new PositionEntity();
            entity.StockId = model.StockId;
            entity.DateOpened = model.DateOpened;
            entity.DateClosed = model.DateClosed;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;

            return entity;
        }

        public IList<PositionModel> ToModel(IList<PositionEntity>? target) => ToList(target, ToModel);
        
        public IList<PositionEntity> ToEntity(IList<PositionV1CreateModel>? target) => ToList(target, ToEntity);
    }
}
