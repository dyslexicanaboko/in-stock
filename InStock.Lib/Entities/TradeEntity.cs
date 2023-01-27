namespace InStock.Lib.Entities
{

    public class TradeEntity : ITrade
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

        public DateTime CreateOnUtc { get; set; }
    }
}
