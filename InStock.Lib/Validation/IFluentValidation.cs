using FluentValidation.Results;

namespace InStock.Lib.Validation
{
  public interface IFluentValidation<in TEntity>
  {
    ValidationResult Validate(TEntity instance);
  }
}
