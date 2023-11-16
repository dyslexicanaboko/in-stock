namespace InStock.Lib.Exceptions;

public static class NotFound
{
  public static NotFoundException Position(int id) => GetNotFound("Position", id, ErrorCodes.NotFound.Position);

  public static NotFoundException Earnings(int id) => GetNotFound("Earnings", id, ErrorCodes.NotFound.Earnings);

  public static NotFoundException Quote(int id) => GetNotFound("Quote", id, ErrorCodes.NotFound.QuoteById);

  public static NotFoundException Quote(string symbol)
    => GetNotFound("Quote", symbol, ErrorCodes.NotFound.QuoteBySymbol);

  public static NotFoundException Stock(int id) => GetNotFound("Stock", id, ErrorCodes.NotFound.StockById);

  public static NotFoundException Portfolio(int userId, int stockId) => GetNotFound($"Portfolio for user {userId} does not contain positions for stock {stockId}.", ErrorCodes.NotFound.PortfolioPosition);

  public static NotFoundException Stock(string symbol) => GetNotFound(
    $"A stock with symbol `{symbol}` could not be found. Consider adding it first.",
    ErrorCodes.NotFound.StockBySymbol);

  public static NotFoundException Symbol(string symbol) => GetNotFound(
    $"`{symbol}` could not be found.",
    ErrorCodes.NotFound.Symbol);

  public static NotFoundException Trade(int id) => GetNotFound("Trade", id, ErrorCodes.NotFound.Trade);

  public static NotFoundException User(int id) => GetNotFound("User", id, ErrorCodes.NotFound.User);
  
  public static NotFoundException UserCredentials() => GetNotFound("User not found", ErrorCodes.NotFound.UserCredentials);

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
