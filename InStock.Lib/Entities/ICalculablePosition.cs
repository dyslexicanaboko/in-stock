namespace InStock.Lib.Entities
{
  public interface ICalculablePosition
  {
    int StockId { get; set; }

    public string Symbol { get; set; }
    DateTime AcquiredOnUtc { get; set; }

    public decimal Shares { get; set; }

    public decimal CostBasis { get; set; }

    public decimal DaysHeld { get; set; }

    public decimal CurrentPrice { get; set; }

    public decimal CurrentValue { get; set; }

    public decimal TotalGain { get; set; }

    public decimal TotalGainPercentage { get; set; }

    public decimal GainRate { get; set; }
  }
}
