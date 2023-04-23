using InStock.Lib.Entities;
using System.Reflection;

namespace InStock.Lib.Models.Client
{
    public class TradeV1CreateModel
    {
        public TradeV1CreateModel(ITrade entity)
        {
            StockId = entity.StockId;
            Price = entity.Price;
            Quantity = entity.Quantity;
            ExecutionDate = entity.ExecutionDate;
            Confirmation = entity.Confirmation;
            TradeTypeId = entity.TradeTypeId;
        }

        public int StockId { get; }

        public int TradeTypeId { get; }

        public decimal Price { get; }

        public decimal Quantity { get; }

        public DateTime ExecutionDate { get; }

        public string? Confirmation { get; }
    }
}
