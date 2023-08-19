namespace InStock.Lib.Entities.Composites
{
  public class PositionComposite
    : PositionEntity, ICalculablePosition, IPositionComposite
  {
    public PositionComposite()
    {
      
    }

    public PositionComposite(string symbol, IPosition position)
    {
      Symbol = symbol;
      PositionId = position.PositionId;
      UserId = position.UserId;
      StockId = position.StockId;
      DateOpened = position.DateOpened;
      DateClosed = position.DateClosed;
      Price = position.Price;
      Quantity = position.Quantity;
    }

    public string Symbol { get; set; }
    public DateTime AcquiredOn { get => DateOpened; set => DateOpened = value; }
    public decimal Shares { get => Quantity; set => Quantity = value; }
    public decimal CostBasis { get; set; }
    public decimal DaysHeld { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal TotalGain { get; set; }
    public decimal TotalGainPercentage { get; set; }
    public decimal GainRate { get; set; }
    public bool IsLongPosition { get; set; }
    public int Rank { get; set; }
  }
}
