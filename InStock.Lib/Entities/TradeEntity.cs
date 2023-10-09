using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Entities
{

    public class TradeEntity 
        : ITrade, IEquatable<TradeEntity>
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
            ExecutionDateUtc = model.ExecutionDateUtc;
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
            ExecutionDateUtc = model.ExecutionDate;
            Confirmation = model.Confirmation;
            TradeTypeId = model.TradeType.TradeTypeId;
        }

        public TradeEntity(int userId, TradeV1CreateModel model)
        {
            UserId = userId;
            StockId = model.StockId;
            Price = model.Price;
            Quantity = model.Quantity;
            ExecutionDateUtc = model.ExecutionDate;
            Confirmation = model.Confirmation;
            TradeTypeId = model.TradeTypeId;
        }

        public TradeEntity(int tradeId, int stockId, TradeV1PatchModel model)
        {
            TradeId = tradeId;
            StockId = stockId;
            Price = model.Price;
            Quantity = model.Quantity;
            ExecutionDateUtc = model.ExecutionDate;
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

        public DateTime ExecutionDateUtc { get; set; }

        public string? Confirmation { get; set; }

        public DateTime CreateOnUtc { get; set; }

        public override bool Equals(object? obj) => Equals(obj as TradeEntity);


        public bool Equals(TradeEntity? other)
        {
            //Check if the right hand argument is null
            if (other is null)
            {
                return false;
            }

            // Optimization for a common success case. If these are the same object, then they are equal.
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (GetType() != other.GetType())
            {
                return false;
            }

            var areEqual =
                UserId == other.UserId &&
                StockId == other.StockId &&
                TradeTypeId == other.TradeTypeId &&
                ExecutionDateUtc == other.ExecutionDateUtc &&
                Price == other.Price &&
                Quantity == other.Quantity;

            return areEqual;
        }

        public override int GetHashCode()
        {
            var hc =
                UserId.GetHashCode() +
                StockId.GetHashCode() +
                TradeTypeId.GetHashCode() +
                ExecutionDateUtc.GetHashCode() +
                Price.GetHashCode() +
                Quantity.GetHashCode();

            return hc;
        }

        public static bool operator ==(TradeEntity? lhs, TradeEntity? rhs)
        {
            if (lhs is not null) return lhs.Equals(rhs);

            return rhs is null;
        }

        public static bool operator !=(TradeEntity lhs, TradeEntity rhs) => !(lhs == rhs);
    }
}
