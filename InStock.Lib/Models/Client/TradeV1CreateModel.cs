using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class TradeV1CreateModel
    {
        public TradeV1CreateModel()
        {
            //Default constructor needed for POST
        }

        public TradeV1CreateModel(ITrade entity)
        {
            StockId = entity.StockId;
            Price = entity.Price;
            Quantity = entity.Quantity;
            ExecutionDate = entity.ExecutionDate;
            Confirmation = entity.Confirmation;
            TradeTypeId = entity.TradeTypeId;
        }

        public int StockId { get; set; }

        public int TradeTypeId { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime ExecutionDate { get; set; }

        public string? Confirmation { get; set; }
    }
}
