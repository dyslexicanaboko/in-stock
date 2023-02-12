using CommunityToolkit.Diagnostics;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;

namespace InStock.Lib.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _repoPosition;
        private readonly IStockRepository _repoStock;

        public PositionService(
            IPositionRepository repoPosition,
            IStockRepository repoStock)
        {
            _repoPosition = repoPosition;

            _repoStock = repoStock;
        }

        public PositionEntity? GetPosition(int id)
        {
            var dbEntity = _repoPosition.Using(x => x.Select(id));

            return dbEntity;
        }

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

        //Position only affects reporting, so if it is deleted, then it just needs to be re-added at worst.
        public void Delete(int positionId)
        {
            _repoPosition.Using(x => x.Delete(positionId));
        }

        public void Delete(int userId, string symbol)
        {
            if (_repoStock.Select(symbol) == null) throw new SymbolNotFoundException(symbol);

            _repoPosition.Using(x => x.Delete(userId, symbol));
        }
    }
}
