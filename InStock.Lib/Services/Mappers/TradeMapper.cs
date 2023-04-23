using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services.Mappers
{
    public class TradeMapper
        : MapperBase, ITradeMapper
    {
        public TradeEntity? ToEntity(TradeModel? model)
        {
            if (model == null) return null;
            
            var entity = new TradeEntity(model);

            return entity;
        }

        public TradeModel? ToModel(TradeEntity? entity)
        {
            if (entity == null) return null;
            
            var model = new TradeModel(entity);

            return model;
        }

        public TradeEntity? ToEntity(ITrade? target)
        {
            if (target == null) return null;
            
            var entity = new TradeEntity(target);

            return entity;
        }

        public TradeModel? ToModel(ITrade? target)
        {
            if (target == null) return null;
            
            var model = new TradeModel(target);
            
            return model;
        }

        public TradeEntity? ToEntity(int userId, TradeV1CreateModel? model)
        {
            if (model == null) return null;

            var entity = new TradeEntity(userId, model);

            return entity;
        }

        public TradeV1FailedCreateModel? ToFailedCreateModel(AddTradeResult? result)
        {
            if (result == null) return null;

            var model = new TradeV1FailedCreateModel(result.Trade);
            model.FailureCode = 100; //This needs to be set correctly
            model.FailureReason = result.GetErrorMessage();

            return model;
        }

        public TradeV1CreateMultipleModel ToModel(IList<AddTradeResult> results)
        {
            var success = results
                .Where(x => x.IsSuccessful)
                .Select(x => ToModel(x.Trade))
                .ToList();

            var failure = results
                .Where(x => !x.IsSuccessful)
                .Select(ToFailedCreateModel)
                .ToList();

            var m = new TradeV1CreateMultipleModel(success, failure);

            return m;
        }

        public IList<TradeModel> ToModel(IList<TradeEntity>? target) => ToList(target, ToModel);

        public IList<TradeEntity> ToEntity(int userId, IList<TradeV1CreateModel>? target) => ToList(userId, target, ToEntity);
    }
}
