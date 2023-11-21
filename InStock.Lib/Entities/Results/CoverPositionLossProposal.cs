namespace InStock.Lib.Entities.Results
{
  public class CoverPositionLossProposal
  {
    public CoverPositionLossProposal(
      int proposal,
      decimal sharesToBuy,
      decimal cost,
      decimal sale,
      GainResult gain)
    {
      Proposal = proposal;
      SharesToBuy = sharesToBuy;
      Cost = cost;
      Sale = sale;
      Gain = gain;
    }

    public int Proposal { get; }

    public decimal SharesToBuy { get; }

    public decimal Cost { get; set; }
    
    public decimal Sale { get; set; }

    public GainResult Gain { get; }
  }
}
