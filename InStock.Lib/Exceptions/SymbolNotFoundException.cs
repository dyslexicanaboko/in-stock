namespace InStock.Lib.Exceptions
{
    public class SymbolNotFoundException
        : BaseException
    {
        public override int ErrorCode { get; set; } = ErrorCodes.SymbolNotFound;


        public SymbolNotFoundException(string symbol)
            : base(GetMessage(symbol))
        {

        }

        private static string GetMessage(string symbol)
        {
            //Should I say where it looked?
            var str = $"{symbol} could not be found.";

            return str;
        }
    }
}
