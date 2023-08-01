namespace InStock.Lib.Entities.Composites;

public interface IPortfolioComposite
{
  int StockId { get; set; }

  string Symbol { get; set; }

  DateTime OwnedAsOf { get; set; }

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

  decimal TotalGainPercentage { get; set; }

  decimal GainRate { get; set; }
}
