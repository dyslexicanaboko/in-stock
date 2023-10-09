namespace InStock.Lib.Entities.Composites;

public interface IPositionComposite
{
  decimal Shares { get; set; }

  decimal CostBasis { get; set; }

  decimal DaysHeld { get; set; }

  decimal CurrentPrice { get; set; }

  decimal CurrentValue { get; set; }

  decimal TotalGain { get; set; }

  decimal TotalGainPercentage { get; set; }

  decimal GainRate { get; set; }

  bool IsLongPosition { get; set; }

  int Rank { get; set; }

  int PositionId { get; set; }

  DateTime DateOpenedUtc { get; set; }

  DateTime? DateClosedUtc { get; set; }

  decimal Price { get; set; }
}
