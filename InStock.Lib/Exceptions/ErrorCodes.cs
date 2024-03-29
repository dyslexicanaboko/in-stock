﻿namespace InStock.Lib.Exceptions
{
  public static class ErrorCodes
  {
    //HTTP 400xx Bad Request - https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/400
    //For when the arguments supplied are invalid
    public static class BadRequest
    {
      public const int Symbol = 40001;

      public const int Null = 40002;

      public const int User = 40003;

      public const int NotGreaterThanZero = 40004;

      public const int NotGreaterThanDateTimeMin = 40005;

      public const int EndDateLessThanStartDate = 40006;
     
      public const int Empty = 40007;
      
      public const int MalformedModel = 40008;
    }

    //HTTP 401xx Unauthorized - https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401
    //When the user does not have permission to be accessing the system period
    public static class Unauthorized
    {
      public const int FailedAuthentication = 40101;
      public const int NotAuthenticated = 40102;
      public const int InvalidPassword = 40103;
    }

    //HTTP 403xx Forbidden - https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/403
    //When the user does not have permission to do what they are trying to do
    public static class Forbidden
    {
      public const int AccessDenied = 40301;
    }


    //HTTP 404xx Not Found - https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/404
    public static class NotFound
    {
      public const int StockById = 40401;

      public const int StockBySymbol = 40402;

      public const int Symbol = 40403;

      public const int Trade = 40404;

      public const int Earnings = 40405;

      public const int Position = 40406;

      public const int QuoteById = 40407;

      public const int QuoteBySymbol = 40408;

      public const int User = 40409;
      
      public const int UserCredentials = 40410;
    }

    //HTTP 500xx Internal Server Error - https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500
    public static class Errors
    {
      public const int EntityExistsAlready = 50001;

      public const int EarningsExistsAlready = 50002;

      public const int MaxEntries = 50003;

      public const int PositionExistsAlready = 50004;

      public const int TradeExistsAlready = 50005;
    }
  }
}
