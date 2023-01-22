using InStock.Lib.Entities;

namespace InStock.Lib.Services;

public interface IPositionService
{
    PositionEntity? GetPosition(int id);
    IList<PositionEntity> GetPosition(int userId, string symbol);
    PositionEntity Add(PositionEntity? position);
    void Delete(int positionId);
    void Delete(int userId, string symbol);
}