using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class StockV1CreatedModel
    {
        public StockV1CreatedModel()
        {
            //Required for controller
        }

        public StockV1CreatedModel(IStock target)
        {
            StockId = target.StockId;
            
            Symbol = target.Symbol;

            Name = target.Name;
        }
        
        public int StockId { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }
    }
}
