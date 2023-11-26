using InStock.Lib.Entities.Composites;

namespace InStock.Lib.Models.Client
{
  public class PositionV1GetCalculatedModel
    : IPositionComposite
  {
    public PositionV1GetCalculatedModel()
    {
      
    }

    public PositionV1GetCalculatedModel(IPositionComposite target)
    {
      PositionId = target.PositionId;
      Shares = target.Shares;
      CostBasis = target.CostBasis;
      DaysHeld = target.DaysHeld;
      CurrentPrice = target.CurrentPrice;
      CurrentValue = target.CurrentValue;
      TotalGain = target.TotalGain;
      TotalGainRate = target.TotalGainRate;
      GainPerDay = target.GainPerDay;
      IsLongPosition = target.IsLongPosition;
      Rank = target.Rank;
      DateOpenedUtc = target.DateOpenedUtc;
      DateClosedUtc = target.DateClosedUtc;
      Price = target.Price;
    }

    public int PositionId { get; set; }
    public decimal Shares { get; set; }
    public decimal CostBasis { get; set; }
    public decimal DaysHeld { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal TotalGain { get; set; }
    public decimal TotalGainRate { get; set; }
    public decimal GainPerDay { get; set; }
    public bool IsLongPosition { get; set; }
    public int Rank { get; set; }
    public DateTime DateOpenedUtc { get; set; }
    public DateTime? DateClosedUtc { get; set; }
    public decimal Price { get; set; }
  }
}
