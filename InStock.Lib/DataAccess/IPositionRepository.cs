using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
    public interface IPositionRepository
        : IRepository<PositionEntity>
    {
        IEnumerable<PositionEntity> Select(int userId, string symbol);

        void Delete(int positionId);
        
        void Delete(int userId, string symbol);
    }
}
