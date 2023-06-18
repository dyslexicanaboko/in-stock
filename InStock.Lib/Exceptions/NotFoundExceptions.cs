namespace InStock.Lib.Exceptions
{
    public static class NotFoundExceptions
    {
        public static NotFoundBaseException Position(int id)
            => GetNotFound("Position", id, ErrorCodes.NotFound.Position);

        public static NotFoundBaseException Earnings(int id)
            => GetNotFound("Earnings", id, ErrorCodes.NotFound.Earnings);

        public static NotFoundBaseException Stock(int id)
            => GetNotFound("Stock", id, ErrorCodes.NotFound.StockId);

        public static NotFoundBaseException Stock(string symbol)
            => GetNotFound($"A stock with symbol `{symbol}` could not be found. Consider adding it first.", ErrorCodes.NotFound.StockSymbol);

        public static NotFoundBaseException Symbol(string symbol)
            => GetNotFound($"`{symbol}` could not be found.", ErrorCodes.NotFound.Symbol);

        public static NotFoundBaseException Trade(int id)
            => GetNotFound("Trade", id, ErrorCodes.NotFound.Trade);

        private static NotFoundBaseException GetNotFound(string subject, object id, int errorCode)
            => GetNotFound($"{subject} with id `{id}` could not be found.", errorCode);

        private static NotFoundBaseException GetNotFound(string message, int errorCode)
        {
            return new NotFoundBaseException(message)
            {
                ErrorCode = errorCode
            };
        }
    }
}
