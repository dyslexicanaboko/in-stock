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
      decimal projectedGain,
      decimal projectedGainPercentage)
    {
      DesiredSalesPrice = desiredSalesPrice;
      Multiplier = multiplier;
      ProposedShares = proposedShares;
      TotalBadShares = totalBadShares;
      CurrentPrice = currentPrice;
      TotalLoss = totalLoss;
      ProjectedGain = projectedGain;
      ProjectedGainPercentage = projectedGainPercentage;
    }

    public decimal DesiredSalesPrice { get; }

    public int Multiplier { get; }

    public decimal ProposedShares { get; }

    public decimal TotalBadShares { get; }

    public decimal CurrentPrice { get; }

    public decimal TotalLoss { get; }

    public decimal ProjectedGain { get; }
    
    public decimal ProjectedGainPercentage { get; }
  }
}
