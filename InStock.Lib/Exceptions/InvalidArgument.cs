using BR = InStock.Lib.Exceptions.ErrorCodes.BadRequest;

namespace InStock.Lib.Exceptions
{
  public static class InvalidArgument
  {
    public static InvalidArgumentException Symbol = new(
      "symbol",
      "Symbol cannot be null, blank or white space.",
      BR.Symbol);

    public static InvalidArgumentException User = new("userId", "Invalid user.", BR.User);

    public static InvalidArgumentException Null(string argument)
      => new(argument, "The provided argument cannot be null.", BR.Null);

    public static InvalidArgumentException Empty(string argument)
      => new(argument, "The provided argument cannot be empty.", BR.Empty);

    public static InvalidArgumentException NotGreaterThanZero(string argument)
      => new(argument, "The provided argument must be greater than zero.", BR.NotGreaterThanZero);

    public static InvalidArgumentException NotGreaterThanDateTimeMin(string argument)
      => new(argument, $"The provided argument must be greater than {DateTime.MinValue}.", BR.NotGreaterThanDateTimeMin);

    public static InvalidArgumentException EndDateLessThanStartDate(string argument)
      => new(argument, "The provided end date argument must be greater than its start date.", BR.EndDateLessThanStartDate);
  }
}
