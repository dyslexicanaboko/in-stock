using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
    public interface IPositionRepository
        : IRepository<PositionEntity>
    {
        IEnumerable<PositionEntity> Select(int userId, string symbol);

        IList<PositionEntity> SelectAll(int stockId, int? exceptPositionId = null);

        void Delete(int positionId);
        
        void Delete(int userId, string symbol);
    }
}
