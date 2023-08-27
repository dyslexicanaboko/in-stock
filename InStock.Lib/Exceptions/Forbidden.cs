using E = InStock.Lib.Exceptions.ErrorCodes.Forbidden;

namespace InStock.Lib.Exceptions
{
  public static class Forbidden
  {
    public static ForbiddenException AccessDenied() => GetForbidden(
      "You do not have access to this resource.",
      E.AccessDenied);

    private static ForbiddenException GetForbidden(string message, int errorCode) => new(message)
    {
      ErrorCode = errorCode
    };
  }
}
