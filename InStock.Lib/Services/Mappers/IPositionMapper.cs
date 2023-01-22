using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers;

public interface IPositionMapper
{
    PositionEntity? ToEntity(PositionModel? model);
    
    PositionEntity? ToEntity(IPosition? target);
    
    PositionEntity? ToEntity(PositionV1CreateModel? model);
    
    PositionModel? ToModel(PositionEntity? entity);
    
    PositionModel? ToModel(IPosition? target);

    IList<PositionModel> ToModel(IList<PositionEntity>? target);
}