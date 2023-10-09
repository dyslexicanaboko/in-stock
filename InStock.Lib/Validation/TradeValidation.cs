using FluentValidation;
using InStock.Lib.Entities;
using static InStock.Lib.Exceptions.InvalidArgument;

namespace InStock.Lib.Validation
{
  public interface ITradeValidation
    : IFluentValidation<TradeEntity>
  {
  }

  public class TradeValidation
    : AbstractValidator<TradeEntity>, ITradeValidation
  {
    public TradeValidation()
    {
      RuleFor(r => r.UserId)
        .GreaterThan(0)
        .WithMessageAndErrorCode(User);

      RuleFor(r => r.StockId)
        .GreaterThan(0)
        .WithMessageAndErrorCode(NotGreaterThanZero(nameof(TradeEntity.StockId)));

      RuleFor(r => r.Price)
        .GreaterThan(0)
        .WithMessageAndErrorCode(NotGreaterThanZero(nameof(TradeEntity.Price)));

      RuleFor(r => r.Quantity)
        .GreaterThan(0)
        .WithMessageAndErrorCode(NotGreaterThanZero(nameof(TradeEntity.Quantity)));

      RuleFor(r => r.ExecutionDateUtc)
        .GreaterThan(DateTime.MinValue)
        .WithMessageAndErrorCode(NotGreaterThanDateTimeMin(nameof(TradeEntity.ExecutionDateUtc)));
    }
  }
}
