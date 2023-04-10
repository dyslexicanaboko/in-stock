using InStock.Lib.Entities;

namespace InStock.Lib.Exceptions
{
    public class EarningsExistsAlreadyException
        : EntityExistsAlreadyException
    {
        public EarningsExistsAlreadyException(string symbol, EarningsEntity earnings)
            : base(GetMessage(symbol, earnings))
        {

        }
        
        private static string GetMessage(string symbol, EarningsEntity earnings)
        {
            var str = 
                $"An earnings entry with symbol \"{symbol}\" already exists with the following parameters:" +
                $"{N}StockId: {earnings.StockId}, " +
                $"{N}Date: {earnings.Date}, " +
                $"{N}Price: {earnings.Order}, " +
                $"{N}Please revise your entry.";

            return str;
        }
    }
}
