using static InStock.Lib.Exceptions.ErrorCodes;

namespace InStock.Lib.Exceptions;

public static class NotFoundExceptions
{
  public static NotFoundException Position(int id) => GetNotFound("Position", id, NotFound.Position);

  public static NotFoundException Earnings(int id) => GetNotFound("Earnings", id, NotFound.Earnings);

  public static NotFoundException Quote(int id) => GetNotFound("Quote", id, NotFound.QuoteById);

  public static NotFoundException Quote(string symbol)
    => GetNotFound("Quote", symbol, NotFound.QuoteBySymbol);

  public static NotFoundException Stock(int id) => GetNotFound("Stock", id, NotFound.StockById);

  public static NotFoundException Stock(string symbol) => GetNotFound(
    $"A stock with symbol `{symbol}` could not be found. Consider adding it first.",
    NotFound.StockBySymbol);

  public static NotFoundException Symbol(string symbol) => GetNotFound(
    $"`{symbol}` could not be found.",
    NotFound.Symbol);

  public static NotFoundException Trade(int id) => GetNotFound("Trade", id, NotFound.Trade);

  public static NotFoundException User(int id) => GetNotFound("User", id, NotFound.User);

  private static NotFoundException GetNotFound(string subject, int id, int errorCode) => GetNotFound(
    $"{subject} with id `{id}` could not be found.",
    errorCode);

  private static NotFoundException GetNotFound(string subject, string symbol, int errorCode) => GetNotFound(
    $"A {subject} with symbol `{symbol}` could not be found.",
    errorCode);

  private static NotFoundException GetNotFound(string message, int errorCode) => new (message)
  {
    ErrorCode = errorCode
  };
}
