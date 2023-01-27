namespace InStock.Lib.Models.Client
{
    public class EarningsV1CreateModel
    {
        public int StockId { get; set; }

        public DateTime Date { get; set; }

        public int Order { get; set; }
    }
}
