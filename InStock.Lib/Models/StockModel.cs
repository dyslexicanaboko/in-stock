using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class StockModel : IStock
    {
        public StockModel(IStock target)
        {
            StockId = target.StockId;
            Symbol = target.Symbol;
            Name = target.Name;
            Notes = target.Notes;
        }

        public StockModel(StockEntity entity)
        {
            StockId = entity.StockId;
            Symbol = entity.Symbol;
            Name = entity.Name;
            Notes = entity.Notes;
            CreateOnUtc = entity.CreateOnUtc;
        }

        public int StockId { get; }

        public string Symbol { get; }

        public string Name { get; }

        public DateTime CreateOnUtc { get; }

        public string? Notes { get; }
    }
}
