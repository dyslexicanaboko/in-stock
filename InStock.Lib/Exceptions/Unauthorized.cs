using E = InStock.Lib.Exceptions.ErrorCodes.Unauthorized;

namespace InStock.Lib.Exceptions
{
  public static class Unauthorized
  {
    public static UnauthorizedException FailedAuthentication() => GetUnauthorized(
      "Authentication failed.",
      E.FailedAuthentication);

    public static UnauthorizedException NotAuthenticated() => GetUnauthorized(
      "Not authenticated. Authenticate and try again.",
      E.NotAuthenticated);

    public static UnauthorizedException InvalidPassword() => GetUnauthorized(
      "The provided username or password is incorrect.",
      E.InvalidPassword);

    private static UnauthorizedException GetUnauthorized(string message, int errorCode) => new(message)
    {
      ErrorCode = errorCode
    };
  }
}
