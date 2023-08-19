using InStock.Lib.Entities;
using InStock.Lib.Entities.Composites;
using InStock.Lib.Models.Results;

namespace InStock.Lib.Services;

public interface IPositionService
{
    PositionEntity? GetPosition(int positionId);
    
    IList<PositionEntity> GetPositions(int userId, string symbol);

    Task<IList<PositionComposite>> GetCalculatedPositions(int userId, string symbol);

    IList<AddPositionResult> Add(PositionEntity position);

    IList<AddPositionResult> Add(IList<PositionEntity>? positions);

    void Edit(PositionEntity position);

    void Delete(int positionId);
    
    void Delete(int userId, string symbol);
}