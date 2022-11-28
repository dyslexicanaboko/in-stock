namespace InStock.Lib.Entities
{
    public interface IStock
    {
        int StockId { get; set; }

        string Symbol { get; set; }

        string Name { get; set; }

        DateTime CreateOnUtc { get; set; }

        string Notes { get; set; }
    }
}
