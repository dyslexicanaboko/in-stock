using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services.Mappers
{
    public interface IEarningsMapper
    {
        EarningsEntity? ToEntity(EarningsModel? model);
        
        EarningsEntity? ToEntity(EarningsV1CreateModel? model);
        
        EarningsEntity? ToEntity(IEarnings? target);
        
        EarningsModel? ToModel(EarningsEntity? entity);
        
        EarningsModel? ToModel(IEarnings? target);

        EarningsV1CreatedModel? ToCreatedModel(EarningsEntity? entity);

        EarningsV1FailedCreateModel? ToFailedCreateModel(AddEarningsResult? result);

        IList<EarningsModel> ToModel(IList<EarningsEntity>? target);

        IList<EarningsEntity> ToEntity(IList<EarningsV1CreateModel>? target);
    }
}