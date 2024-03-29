﻿using InStock.Lib.Entities.Composites;

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
      TotalGainPercentage = target.TotalGainPercentage;
      GainRate = target.GainRate;
      IsLongPosition = target.IsLongPosition;
      Rank = target.Rank;
      DateOpened = target.DateOpened;
      DateClosed = target.DateClosed;
      Price = target.Price;
    }

    public int PositionId { get; set; }
    public decimal Shares { get; set; }
    public decimal CostBasis { get; set; }
    public decimal DaysHeld { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal TotalGain { get; set; }
    public decimal TotalGainPercentage { get; set; }
    public decimal GainRate { get; set; }
    public bool IsLongPosition { get; set; }
    public int Rank { get; set; }
    public DateTime DateOpened { get; set; }
    public DateTime? DateClosed { get; set; }
    public decimal Price { get; set; }
  }
}
