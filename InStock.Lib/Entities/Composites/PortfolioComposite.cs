namespace InStock.Lib.Entities.Composites
{
  /// <summary>
  /// Rolled up data that represents the position overview in a portfolio. Each portfolio object includes calculated
  /// fields that are not stored in the database.
  /// </summary>
  public class PortfolioComposite
    : IPortfolioComposite, ICalculablePosition
  {
    //Database fields
    public int StockId { get; set; }
    public string Symbol { get; set; }
    public DateTime AcquiredOnUtc { get; set; }
    public decimal Shares { get; set; }
    public decimal CostBasis { get; set; }
    public decimal LowestHeld { get; set; }
    public decimal HighestHeld { get; set; }
    public decimal AveragePrice { get; set; }
    
    public int Short { get; set; }
    public int Long { get; set; }

    //Calculated fields
    public decimal DaysHeld { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal TotalGain { get; set; }
    public decimal TotalGainRate { get; set; }
    public decimal GainRate { get; set; }
  }
}
