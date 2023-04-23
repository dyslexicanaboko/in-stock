using InStock.Lib.Entities;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Models
{
    public class TradeModel 
    {
        public TradeModel(ITrade entity)
        {
            TradeId = entity.TradeId;
            UserId = entity.UserId;
            StockId = entity.StockId;
            Price = entity.Price;
            Quantity = entity.Quantity;
            ExecutionDate = entity.ExecutionDate;
            Confirmation = entity.Confirmation;

            TradeType = new TradeTypeV1Model(entity.TradeTypeId);
        }

        public TradeModel(TradeEntity entity)
        {
            TradeId = entity.TradeId;
            UserId = entity.UserId;
            StockId = entity.StockId;
            Price = entity.Price;
            Quantity = entity.Quantity;
            ExecutionDate = entity.ExecutionDate;
            Confirmation = entity.Confirmation;

            TradeType = new TradeTypeV1Model(entity.TradeType);
        }

        public int TradeId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime ExecutionDate { get; set; }

        public string? Confirmation { get; set; }

        public TradeTypeV1Model TradeType { get; set; }
    }
}
