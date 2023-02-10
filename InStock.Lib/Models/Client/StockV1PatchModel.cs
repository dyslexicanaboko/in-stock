namespace InStock.Lib.Models.Client
{
    public class StockV1PatchModel
    {
        //public StockV1PatchModel(int stockId, string? notes)
        //{
        //    StockId = stockId;

        //    Notes = notes;
        //}

        public int StockId { get; set; }

        public string? Notes { get; set; }
    }
}
