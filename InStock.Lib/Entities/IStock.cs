namespace InStock.Lib.Entities
{
    public interface IStock
    {
        int StockId { get; }

        string Symbol { get; }

        string Name { get; }

        string? Notes { get; }
    }
}
