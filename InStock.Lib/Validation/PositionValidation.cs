using FluentValidation;
using InStock.Lib.Entities;
using static InStock.Lib.Exceptions.InvalidArgument;

namespace InStock.Lib.Validation
{
  public interface IPositionValidation
    : IFluentValidation<PositionEntity>
  {
  }

  public class PositionValidation
    : AbstractValidator<PositionEntity>, IPositionValidation
  {
    public PositionValidation()
    {
      RuleFor(r => r.UserId)
        .GreaterThan(0)
        .WithMessageAndErrorCode(User);

      RuleFor(r => r.StockId)
        .GreaterThan(0)
        .WithMessageAndErrorCode(NotGreaterThanZero(nameof(PositionEntity.StockId)));

      RuleFor(r => r.Price)
        .GreaterThan(0)
        .WithMessageAndErrorCode(NotGreaterThanZero(nameof(PositionEntity.Price)));

      RuleFor(r => r.Quantity)
        .GreaterThan(0)
        .WithMessageAndErrorCode(NotGreaterThanZero(nameof(PositionEntity.Quantity)));

      RuleFor(r => r.DateOpenedUtc)
        .GreaterThan(DateTime.MinValue)
        .WithMessageAndErrorCode(NotGreaterThanDateTimeMin(nameof(PositionEntity.DateOpenedUtc)))
        .DependentRules(
          () =>
          {
            RuleFor(r => r.DateClosedUtc)
              .Must(
                (entity, dateClosed) =>
                {
                  if (!dateClosed.HasValue) return true;

                  return Validations.IsEndDateGreaterThanStartDate(
                      dateClosed.Value,
                      entity.DateOpenedUtc,
                      nameof(entity.DateClosedUtc),
                      false) ==
                    null;
                })
              .WithMessageAndErrorCode(EndDateLessThanStartDate(nameof(PositionEntity.DateClosedUtc)));
          });
    }
  }
}
