namespace InStock.Lib.Entities.Results
{
  public class CoverPositionLossResult
  {
    public CoverPositionLossResult(
      decimal desiredSalesPrice,
      decimal totalShares,
      decimal badShares,
      decimal currentPrice,
      decimal currentLoss)
    {
      DesiredSalesPrice = desiredSalesPrice;
      TotalShares = totalShares;
      BadShares = badShares;
      CurrentPrice = currentPrice;
      CurrentLoss = currentLoss;
    }

    public decimal DesiredSalesPrice { get; }

    public decimal TotalShares { get; }
    
    public decimal BadShares { get; }

    public decimal CurrentPrice { get; }

    public decimal CurrentLoss { get; }

    public List<CoverPositionLossProposal> Proposals { get; } = new ();
  }
}
