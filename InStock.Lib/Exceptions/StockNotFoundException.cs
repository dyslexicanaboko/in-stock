namespace InStock.Lib.Exceptions
{
    public class StockNotFoundException
        : Exception
    {
        public StockNotFoundException(string symbol)
            : base(GetMessage(symbol))
        {

        }

        public StockNotFoundException(int stockId)
            : base(GetMessage(stockId))
        {

        }

        private static string GetMessage(int stockId)
        {
            var str = $"A stock with id \"{stockId}\" could not be found.";

            return str;
        }

        private static string GetMessage(string symbol)
        {
            //Should I say where it looked?
            var str = $"A stock with symbol \"{symbol}\" could not be found. Consider adding it first.";

            return str;
        }
    }
}
