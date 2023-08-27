using InStock.Lib.Entities;
using InStock.Lib.Entities.Composites;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services.Mappers;

public interface IPositionMapper
{
    PositionEntity? ToEntity(PositionModel? model);
    
    PositionEntity? ToEntity(IPosition? target);
    
    PositionEntity? ToEntity(int userId, PositionV1CreateModel? model);
    
    PositionModel? ToModel(PositionEntity? entity);
    
    PositionV1CreateModel? ToCreateModel(PositionEntity? entity);
    
    PositionV1FailedCreateModel? ToFailedCreateModel(AddPositionResult? result);

    PositionModel? ToModel(IPosition? target);

    PositionV1CreateMultipleModel ToModel(IList<AddPositionResult> results);

    IList<PositionModel> ToModel(IList<PositionEntity>? target);

    IList<PositionEntity> ToEntity(int userId, IList<PositionV1CreateModel>? target);
    
    PositionV1PatchModel? ToPatchModel(PositionEntity? entity);

    PositionEntity? ToEntity(int positionId, int stockId, PositionV1PatchModel? model);

    IList<PositionV1GetCalculatedModel> ToCalculatedModel(IList<PositionComposite>? target);
}