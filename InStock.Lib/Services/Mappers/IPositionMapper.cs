using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services.Mappers;

public interface IPositionMapper
{
    PositionEntity? ToEntity(PositionModel? model);
    
    PositionEntity? ToEntity(IPosition? target);
    
    PositionEntity? ToEntity(PositionV1CreateModel? model);
    
    PositionModel? ToModel(PositionEntity? entity);
    
    PositionV1CreateModel? ToCreateModel(PositionEntity? entity);
    
    PositionV1FailedCreateModel? ToFailedCreateModel(AddPositionResult? result);

    PositionModel? ToModel(IPosition? target);

    IList<PositionModel> ToModel(IList<PositionEntity>? target);
    
    IList<PositionEntity> ToEntity(IList<PositionV1CreateModel>? target);
}