using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

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

        IList<EarningsModel> ToModel(IList<EarningsEntity>? target);
    }
}