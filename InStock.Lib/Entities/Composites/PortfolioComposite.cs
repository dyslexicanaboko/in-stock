namespace InStock.Lib.Entities.Composites
{
  public class PortfolioComposite
    : IPortfolioComposite
  {
    //Database fields
    public int StockId { get; set; }
    public string Symbol { get; set; }
    public DateTime OwnedAsOf { get; set; }
    public decimal Shares { get; set; }
    public decimal CostBasis { get; set; }
    public decimal LowestHeld { get; set; }
    public decimal HighestHeld { get; set; }
    public int Short { get; set; }
    public int Long { get; set; }

    //Calculated fields
    public decimal DaysHeld { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal TotalGain { get; set; }
    public decimal TotalGainPercentage { get; set; }
    public decimal GainRate { get; set; }
  }
}
