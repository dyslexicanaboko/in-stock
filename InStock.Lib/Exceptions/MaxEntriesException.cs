namespace InStock.Lib.Exceptions
{
    public class MaxEntriesException
        : BaseException
    {
        public override int ErrorCode { get; set; } = ErrorCodes.Errors.MaxEntries;


        public MaxEntriesException(string symbol, string subject, int max)
            : base(GetMessage(symbol, subject, max))
        {

        }

        private static string GetMessage(string symbol, string subject, int max)
        {
            var str = $"Stock `{symbol}` already has a max of {max} {subject} entries.";

            return str;
        }
    }
}
