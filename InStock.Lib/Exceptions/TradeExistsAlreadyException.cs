using InStock.Lib.Entities;

namespace InStock.Lib.Exceptions
{
    public class TradeExistsAlreadyException
        : EntityExistsAlreadyException
    {
        public override int ErrorCode { get; set; } = ErrorCodes.TradeExistsAlready;


        public TradeExistsAlreadyException(string symbol, TradeEntity trade)
            : base(GetMessage(symbol, trade))
        {

        }
        
        private static string GetMessage(string symbol, TradeEntity trade)
        {
            var str = 
                $"A position with symbol \"{symbol}\" already exists with the following parameters:" +
                $"{N}UserId: {trade.UserId}, " +
                $"{N}StockId: {trade.StockId}, " +
                $"{N}TradeTypeId: {trade.TradeTypeId} ({trade.TradeType})," +
                $"{N}ExecutionDate: {trade.ExecutionDate}, " +
                $"{N}Price: {trade.Price}, " +
                $"{N}Quantity: {trade.Quantity}, " +
                $"{N}Please revise your entry.";

            return str;
        }
    }
}
