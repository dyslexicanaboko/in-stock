namespace InStock.Lib.Entities
{
    public interface IEarnings
    {
        int EarningsId { get; set; }

        int StockId { get; set; }

        DateTime Date { get; set; }

        int Order { get; set; }
    }
}
