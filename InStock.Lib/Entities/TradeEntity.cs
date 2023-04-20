using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Entities
{

    public class TradeEntity : ITrade
    {
        public TradeEntity()
        {

        }

        public TradeEntity(ITrade model)
        {
            TradeId = model.TradeId;
            UserId = model.UserId;
            StockId = model.StockId;
            Price = model.Price;
            Quantity = model.Quantity;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Confirmation = model.Confirmation;
            TradeTypeId = model.TradeTypeId;
        }

        public TradeEntity(TradeModel model)
        {
            TradeId = model.TradeId;
            UserId = model.UserId;
            StockId = model.StockId;
            Price = model.Price;
            Quantity = model.Quantity;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Confirmation = model.Confirmation;
            TradeTypeId = model.TradeType.TradeTypeId;
        }

        public TradeEntity(int userId, TradeV1CreateModel model)
        {
            UserId = userId;
            StockId = model.StockId;
            Price = model.Price;
            Quantity = model.Quantity;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Confirmation = model.Confirmation;
            TradeTypeId = model.TradeTypeId;
        }

        public int TradeId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }

        public int TradeTypeId { get; set; }

        public TradeType TradeType
        {
            get => (TradeType)TradeTypeId;
            set => TradeTypeId = Convert.ToInt32(value);
        }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Confirmation { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
