using InStock.Lib.Entities;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services;

public interface IPositionService
{
    PositionEntity? GetPosition(int id);
    
    IList<PositionEntity> GetPosition(int userId, string symbol);

    IList<AddPositionResult> Add(PositionEntity position);

    IList<AddPositionResult> Add(IList<PositionEntity>? positions);

    void Delete(int positionId);
    
    void Delete(int userId, string symbol);
}