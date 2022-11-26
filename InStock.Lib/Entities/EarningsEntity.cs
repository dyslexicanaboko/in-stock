namespace InStock.Lib.Entities
{

    public class EarningsEntity : IEarnings
    {
        public int EarningsId { get; set; }

        public int StockId { get; set; }

        public DateTime Date { get; set; }

        public int Order { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
