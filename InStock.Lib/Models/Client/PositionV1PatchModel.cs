using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class PositionV1PatchModel
    {
        public PositionV1PatchModel()
        {
            //Required for controller
        }

        public PositionV1PatchModel(IPosition entity)
        {
            DateOpened = entity.DateOpened;
            DateClosed = entity.DateClosed;
            Price = entity.Price;
            Quantity = entity.Quantity;
        }

        public DateTime DateOpened { get; set; }

        public DateTime? DateClosed { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
