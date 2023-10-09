using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class TradeV1PatchModel
    {
        public TradeV1PatchModel()
        {
            //Required for controller
        }

        public TradeV1PatchModel(ITrade entity)
        {
            TradeTypeId = entity.TradeTypeId;
            Price = entity.Price;
            Quantity = entity.Quantity;
            ExecutionDate = entity.ExecutionDateUtc;
            Confirmation = entity.Confirmation;
        }

        public int TradeTypeId { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime ExecutionDate { get; set; }

        public string? Confirmation { get; set; }
    }
}
