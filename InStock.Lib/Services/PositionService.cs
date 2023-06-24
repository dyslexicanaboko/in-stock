using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Models.Results;
using InStock.Lib.Validation;

namespace InStock.Lib.Services
{
  public class PositionService : IPositionService
  {
    private readonly IPositionRepository _repoPosition;

    private readonly IStockRepository _repoStock;

    private readonly IPositionValidation _validation;

    public PositionService(
      IPositionRepository repoPosition,
      IStockRepository repoStock,
      IPositionValidation validation)
    {
      _repoPosition = repoPosition;

      _repoStock = repoStock;

      _validation = validation;
    }

    public PositionEntity? GetPosition(int positionId)
    {
      Validations.IsGreaterThanZero(positionId, nameof(positionId));

      var dbEntity = _repoPosition.Using(x => x.Select(positionId));

      return dbEntity;
    }

    //I am not going to check for the symbol (for now) in cases like this because it's non-consequential.
    //I might change my mind later.
    public IList<PositionEntity> GetPosition(int userId, string symbol)
    {
      Validations.ThrowOnError(
        () => Validations.IsUserIdValid(userId, false),
        () => Validations.IsSymbolValid(symbol, false));

      var lst = _repoPosition
        .Using(x => x.Select(userId, symbol))
        .ToList();

      return lst;
    }

    public IList<AddPositionResult> Add(PositionEntity position)
      => Add(new List<PositionEntity> { position });

    public IList<AddPositionResult> Add(IList<PositionEntity>? positions)
    {
      Validations.IsNotNull(positions, nameof(positions));

      //Get a distinct list of Stock Ids as there is no guarantee they are the same
      var stockIds = positions.Select(x => x.StockId).Distinct().ToList();

      //Get all stocks to prove they exist and order them by Stock Id so that the positions
      //are processed in stock order (grouped by stockId)
      var stocks = _repoStock
        .Using(x => x.Select(stockIds))
        .OrderBy(x => x.StockId)
        .ToList();

      var positionsSorted = positions.OrderBy(x => x.StockId).ToList();

      var results = new List<AddPositionResult>(positions.Count);

      foreach (var p in positionsSorted)
      {
        var r = new AddPositionResult(p);

        try
        {
          var s = stocks.SingleOrDefault(x => x.StockId == p.StockId);

          //Stock must exist before attempting to make positions with it
          if (s == null)
            throw NotFoundExceptions.Stock(p.StockId);

          r.Position = Add(s.Symbol, p);

          r.Success();
        }
        catch (Exception ex)
        {
          r.Failure(ex);
        }

        results.Add(r);
      }

      return results;
    }

    public void Edit(PositionEntity position)
    {
      Validations.IsNotNull(position, nameof(position));

      //Not allowing the user to change the stock the position is associated with, therefore this must exist
      var stock = _repoStock.Using(x => x.Select(position.StockId));

      using (_repoPosition)
      {
        //To enforce uniqueness, all position must be returned so a compare can be performed
        //Exclude the current Position row on purpose because it is being edited
        var lstPosition = _repoPosition.SelectAll(position.StockId, position.PositionId);

        //Reject duplicate entries
        CheckForDuplicates(lstPosition, position, stock!.Symbol);

        _repoPosition.Update(position);
      }
    }

    //Position only affects reporting, so if it is deleted, then it just needs to be re-added at worst.
    public void Delete(int positionId)
    {
      Validations.IsGreaterThanZero(positionId, nameof(positionId));

      _repoPosition.Using(x => x.Delete(positionId));
    }

    public void Delete(int userId, string symbol)
    {
      Validations.ThrowOnError(
        () => Validations.IsUserIdValid(userId, false),
        () => Validations.IsSymbolValid(symbol, false));

      if (_repoStock.Using(x => x.Select(symbol)) is null) throw NotFoundExceptions.Symbol(symbol);

      _repoPosition.Using(x => x.Delete(userId, symbol));
    }

    private static void CheckForDuplicates(
      IEnumerable<PositionEntity> existingPositions,
      PositionEntity position,
      string symbol)
    {
      var dup = existingPositions.Any(x => x == position);

      if (!dup) return;

      throw new PositionExistsAlreadyException(symbol, position);
    }

    private PositionEntity Add(string symbol, PositionEntity? position)
    {
      //TODO: Abstract this concept? Null check, validate, test, throw
      Validations.IsNotNull(position, nameof(position));
      
      var result = _validation.Validate(position);

      if (!result.IsValid) throw new BadRequestException(result.Errors);
      
      using (_repoPosition)
      {
        //For now, not going to store existing positions in memory on the off chance positions
        //are being added via different threads somehow for the same user. Low chance, but possible.
        var positions = _repoPosition.Select(position.UserId, symbol).ToList();

        //To avoid breaking unique constraints check if existing positions do not conflict
        if (positions.Any(x => x == position)) throw new PositionExistsAlreadyException(symbol, position);

        //If this is a unique position then insert it
        position.PositionId = _repoPosition.Insert(position);
      }

      return position;
    }
  }
}
