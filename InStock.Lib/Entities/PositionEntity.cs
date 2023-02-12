namespace InStock.Lib.Entities
{

    public class PositionEntity 
        : IPosition, IEquatable<PositionEntity>
    {
        public int PositionId { get; set; }

        public int UserId { get; set; }

        public int StockId { get; set; }

        public DateTime DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public DateTime CreateOnUtc { get; set; }

        public override bool Equals(object? obj) => Equals(obj as PositionEntity);


        public bool Equals(PositionEntity? other)
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
                DateOpened == other.DateOpened &&
                DateClosed == other.DateClosed &&
                Price == other.Price &&
                Quantity == other.Quantity;

            return areEqual;
        }
        
        public override int GetHashCode()
        {
            var hc =
                UserId.GetHashCode() +
                StockId.GetHashCode() +
                DateOpened.GetHashCode() +
                DateClosed.GetHashCode() + //When this is null it's zero
                Price.GetHashCode() +
                Quantity.GetHashCode();

            return hc;
        }

        public static bool operator ==(PositionEntity? lhs, PositionEntity? rhs)
        {
            if (lhs is not null) return lhs.Equals(rhs);
            
            return rhs is null;
        }

        public static bool operator !=(PositionEntity lhs, PositionEntity rhs) => !(lhs == rhs);
    }
}
