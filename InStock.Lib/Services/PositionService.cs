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
            Guard.IsGreaterThan(id, 0, nameof(id));
            
            var dbEntity = _repoPosition.Using(x => x.Select(id));

            return dbEntity;
        }

        //I am not going to check for the symbol (for now) in cases like this because it's non-consequential.
        //I might change my mind later.
        public IList<PositionEntity> GetPosition(int userId, string symbol)
        {
            Guard.IsGreaterThan(userId, 0, nameof(userId));
            Guard.IsNotNullOrWhiteSpace(symbol, nameof(symbol));

            var lst = _repoPosition
                .Using(x => x.Select(userId, symbol))
                .ToList();

            return lst;
        }
        
        public PositionEntity Add(PositionEntity? position)
        {
            Guard.IsNotNull(position);
            Guard.IsGreaterThan(position.UserId, 0, nameof(position.UserId));
            Guard.IsGreaterThan(position.StockId, 0, nameof(position.StockId));
            Guard.IsGreaterThan(position.Price, 0, nameof(position.Price));
            Guard.IsGreaterThan(position.Quantity, 0, nameof(position.Quantity));
            Guard.IsGreaterThan(position.DateOpened, DateTime.MinValue, nameof(position.DateOpened));

            if (position.DateClosed.HasValue)
            {
                Guard.IsGreaterThanOrEqualTo(position.DateClosed.Value, position.DateOpened, nameof(position.DateClosed));
            }

            //Stock must exist before attempting to make positions with it
            var stock = _repoStock.Using(x => x.Select(position.StockId));

            if (stock is null) throw new StockNotFoundException(position.StockId);

            using (_repoPosition)
            {
                var positions = _repoPosition.Select(position.UserId, stock.Symbol).ToList();

                //To avoid breaking unique constraints check if existing positions do not conflict
                if (positions.Any(x => x == position)) throw new PositionExistsAlreadyException(stock.Symbol, position);

                //If this is a unique position then insert it
                position.PositionId = _repoPosition.Insert(position);
            }

            return position;
        }

        //Position only affects reporting, so if it is deleted, then it just needs to be re-added at worst.
        public void Delete(int positionId)
        {
            Guard.IsGreaterThan(positionId, 0, nameof(positionId));
            
            _repoPosition.Using(x => x.Delete(positionId));
        }

        public void Delete(int userId, string symbol)
        {
            Guard.IsGreaterThan(userId, 0, nameof(userId));
            Guard.IsNotNullOrWhiteSpace(symbol, nameof(symbol));
            
            if (_repoStock.Using(x => x.Select(symbol)) is null) throw new SymbolNotFoundException(symbol);

            _repoPosition.Using(x => x.Delete(userId, symbol));
        }
    }
}
