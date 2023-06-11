namespace InStock.Lib.Exceptions
{
    public class TradeNotFoundException
        : NotFoundBaseException
    {
        public override int ErrorCode { get; set; } = ErrorCodes.TradeNotFound;

        public TradeNotFoundException(int tradeId)
            : base(GetMessage(tradeId))
        {

        }

        private static string GetMessage(int tradeId)
        {
            var str = $"A trade with id \"{tradeId}\" could not be found.";

            return str;
        }
    }
}
