namespace InStock.Lib.Exceptions
{
    public class StockNotFoundException
        : Exception
    {
        public StockNotFoundException(string symbol)
            : base(GetMessage(symbol))
        {

        }

        private static string GetMessage(string symbol)
        {
            //Should I say where it looked?
            var str = $"A stock with symbol \"{symbol}\" could not be found. Consider adding it first.";

            return str;
        }
    }
}
