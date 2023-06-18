using InStock.Lib.Entities;

namespace InStock.Lib.Exceptions
{
    public class PositionExistsAlreadyException
        : EntityExistsAlreadyException
    {
        public override int ErrorCode { get; set; } = ErrorCodes.Errors.PositionExistsAlready;


        public PositionExistsAlreadyException(string symbol, PositionEntity position)
            : base(GetMessage(symbol, position))
        {

        }
        
        private static string GetMessage(string symbol, PositionEntity position)
        {
            var str = 
                $"A position with symbol `{symbol}` already exists with the following parameters:" +
                $"{N}UserId: {position.UserId}, " +
                $"{N}StockId: {position.StockId}, " +
                $"{N}DateOpened: {position.DateOpened}, " +
                $"{N}DateClosed: {position.DateClosed}, " +
                $"{N}Price: {position.Price}, " +
                $"{N}Quantity: {position.Quantity}, " +
                $"{N}Please revise your entry.";

            return str;
        }
    }
}
