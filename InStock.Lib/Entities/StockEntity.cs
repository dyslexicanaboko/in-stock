namespace InStock.Lib.Entities
{

    public class StockEntity : IStock
    {
        public StockEntity()
        {

        }

        public StockEntity(IStock target)
        {
            StockId = target.StockId;
            Symbol = target.Symbol;
            Name = target.Name;
            Notes = target.Notes;
        }
        
        public int StockId { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public DateTime CreateOnUtc { get; set; }

        public string? Notes { get; set; }
    }
}
