using InStock.Lib.Entities;

namespace InStock.Lib.Models.Results
{
    public class AddEarningsResult
        : ResultBase
    {
        public AddEarningsResult(EarningsEntity earnings)
        {
            Earnings = earnings;
        }

        public EarningsEntity Earnings { get; set; }
    }
}
