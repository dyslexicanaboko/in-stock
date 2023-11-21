namespace InStock.Lib.Entities.Composites;

public interface IPortfolioComposite
{
  int StockId { get; set; }

  string Symbol { get; set; }

  DateTime AcquiredOnUtc { get; set; }

  decimal Shares { get; set; }

  decimal CostBasis { get; set; }

  decimal LowestHeld { get; set; }

  decimal HighestHeld { get; set; }

  int Short { get; set; }

  int Long { get; set; }

  decimal DaysHeld { get; set; }

  decimal CurrentPrice { get; set; }

  decimal CurrentValue { get; set; }

  decimal TotalGain { get; set; }

  decimal TotalGainRate { get; set; }

  decimal GainRate { get; set; }
}
