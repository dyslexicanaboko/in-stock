namespace InStock.Lib.Models.Client
{
    public class EarningsV1PatchModel
    {
        public int EarningsId { get; set; }

        public DateTime Date { get; set; }
        
        public int Order { get; set; }
    }
}
