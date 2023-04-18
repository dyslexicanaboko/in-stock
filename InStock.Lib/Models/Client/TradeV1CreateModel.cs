namespace InStock.Lib.Models.Client
{
    public class TradeV1CreateModel
    {
        public int StockId { get; set; }

        public int TradeTypeId { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Confirmation { get; set; }
    }
}
