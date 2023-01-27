using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class TradeModel : ITrade
    {
        public int TradeId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }

        public bool Type { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Confirmation { get; set; }
    }
}
