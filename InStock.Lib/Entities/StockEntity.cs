namespace InStock.Lib.Entities
{

    public class StockEntity : IStock
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StockEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }

        public StockEntity(IStock target)
        {
            Symbol = target.Symbol;

            Name = target.Name;
        }
        
        public int StockId { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public DateTime CreateOnUtc { get; set; }

        public string? Notes { get; set; }
    }
}
