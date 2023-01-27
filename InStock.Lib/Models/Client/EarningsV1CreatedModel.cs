namespace InStock.Lib.Models.Client
{
    public class EarningsV1CreatedModel
    {
        public int EarningsId { get; set; }

        public int StockId { get; set; }

        public DateTime Date { get; set; }

        public int Order { get; set; }
    }
}
