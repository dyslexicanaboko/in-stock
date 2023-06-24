using CommunityToolkit.Diagnostics;
using InStock.Lib.Exceptions;
using static InStock.Lib.Exceptions.ErrorCodes;

namespace InStock.Lib.Validation
{
  public static class CommonValidations
  {
    public static InvalidArgumentException? IsSymbolValid(string symbol, bool raiseException = true)
      => GetInvalidArgumentException(
        () => Guard.IsNotNullOrWhiteSpace(symbol),
        raiseException,
        nameof(symbol),
        "Symbol cannot be null, blank or white space.",
        BadRequest.Symbol);

    public static InvalidArgumentException? IsNotNull<T>(T? value, string argument, bool raiseException = true)
      => GetInvalidArgumentException(
        () => Guard.IsNotNull(value),
        raiseException,
        argument,
        "The provided argument cannot be null.",
        BadRequest.Null);

    private static InvalidArgumentException? GetInvalidArgumentException(
      Action validationTest,
      bool raiseException,
      string argument,
      string message,
      int errorCode)
    {
      //This is a hack until I come up with a better way to handle this.
      //I like the Guard syntax, but it automatically raises exceptions.
      try
      {
        validationTest();

        return null;
      }
      catch
      {
        var ex = new InvalidArgumentException(argument, message, errorCode);

        if (raiseException) throw ex;

        return ex;
      }
    }
  }
}
