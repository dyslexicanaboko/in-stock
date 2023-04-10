namespace InStock.Lib.Exceptions
{
    public class MaxEntriesException
        : Exception
    {
        public MaxEntriesException(string symbol, string subject, int max)
            : base(GetMessage(symbol, subject, max))
        {

        }

        private static string GetMessage(string symbol, string subject, int max)
        {
            var str = $"Stock \"{symbol}\" already has a max of {max} {subject} entries.";

            return str;
        }
    }
}
