using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class StockModel : IStock
    {
        public int StockId { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public DateTime CreateOnUtc { get; set; }

        public string Notes { get; set; }
    }
}
