using InStock.Lib.Entities;

namespace InStock.Lib.Exceptions
{
    public class EarningsExistsAlreadyException
        : EntityExistsAlreadyException
    {
        public enum Part
        {
            Date,
            Order
        }

        public EarningsExistsAlreadyException(string symbol, EarningsEntity earnings, Part part)
            : base(GetMessage(symbol, earnings, part))
        {

        }
        
        private static string GetMessage(string symbol, EarningsEntity earnings, Part part)
        {
            var str =
                $"An earnings entry with symbol \"{symbol}\" already exists with the following parameters:" +
                $"{N}StockId: {earnings.StockId}, " +
                (part == Part.Date ? $"{N}Date: {earnings.Date}, " : $"{N}Order: {earnings.Order}, ") +
                $"{N}Please revise your entry.";

            return str;
        }
    }
}
