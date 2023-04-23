namespace InStock.Lib.Entities
{
    public interface IEarnings
    {
        int EarningsId { get; }

        int StockId { get; }

        DateTime Date { get; }

        int Order { get; }
    }
}
