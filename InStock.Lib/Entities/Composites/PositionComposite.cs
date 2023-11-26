﻿namespace InStock.Lib.Entities.Composites
{
  /// <summary>
  /// Represents the core data that makes up a position, but also includes calculated fields that are not
  /// stored in the database. One object represents only a single row, position or tax-lot.
  /// </summary>
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
      DateOpenedUtc = position.DateOpenedUtc;
      DateClosedUtc = position.DateClosedUtc;
      Price = position.Price;
      Quantity = position.Quantity;
    }

    public string Symbol { get; set; }
    public DateTime AcquiredOnUtc { get => DateOpenedUtc; set => DateOpenedUtc = value; }
    public decimal Shares { get => Quantity; set => Quantity = value; }
    public decimal CostBasis { get; set; }
    public decimal DaysHeld { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal TotalGain { get; set; }
    public decimal TotalGainRate { get; set; }
    public decimal GainPerDay { get; set; }
    public bool IsLongPosition { get; set; }
    public int Rank { get; set; }
  }
}
