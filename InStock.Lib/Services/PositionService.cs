using CommunityToolkit.Diagnostics;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _repoPosition;

        public PositionService(
            IPositionRepository repoPosition)
        {
            _repoPosition = repoPosition;
        }

        public PositionEntity? GetPosition(int id)
        {
            var dbEntity = _repoPosition.Using(x => x.Select(id));

            return dbEntity;
        }

        //TODO: Need to check for existence of the symbol?
        public IList<PositionEntity> GetPosition(int userId, string symbol)
        {
            var lst = _repoPosition
                .Using(x => x.Select(userId, symbol))
                .ToList();

            return lst;
        }

        //TODO: Prevent duplicate entries of order or dates
        //TODO: Enforce FK of StockId existence
        public PositionEntity Add(PositionEntity? position)
        {
            Guard.IsNotNull(position);
            
            position.PositionId = _repoPosition.Using(x => x.Insert(position));

            return position;
        }

        //Position only affects reporting, so if it is deleted, then it just needs to be re-added.
        public void Delete(int positionId)
        {
            _repoPosition.Using(x => x.Delete(positionId));
        }

        //TODO: Need to check for existence of the symbol?
        public void Delete(int userId, string symbol)
        {
            _repoPosition.Using(x => x.Delete(userId, symbol));
        }
    }
}
