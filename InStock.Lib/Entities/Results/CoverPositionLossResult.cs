namespace InStock.Lib.Entities.Results
{
  public class CoverPositionLossResult
  {
    public CoverPositionLossResult(
      decimal desiredSalesPrice,
      int multiplier,
      decimal proposedShares,
      decimal totalBadShares,
      decimal currentPrice,
      decimal totalLoss,
      GainResult projectedGain)
    {
      DesiredSalesPrice = desiredSalesPrice;
      Multiplier = multiplier;
      ProposedShares = proposedShares;
      TotalBadShares = totalBadShares;
      CurrentPrice = currentPrice;
      TotalLoss = totalLoss;
      ProjectedGain = projectedGain;
    }

    public decimal DesiredSalesPrice { get; }

    public int Multiplier { get; }

    public decimal ProposedShares { get; }

    public decimal TotalBadShares { get; }

    public decimal CurrentPrice { get; }

    public decimal TotalLoss { get; }

    public GainResult ProjectedGain { get; }
  }
}
