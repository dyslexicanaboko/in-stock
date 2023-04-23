using InStock.Lib.Entities;

namespace InStock.Lib.Models.Results
{
    public class AddPositionResult
        : ResultBase
    {
        public AddPositionResult(PositionEntity position)
        {
            Position = position;
        }

        public PositionEntity Position { get; set; }
    }
}
