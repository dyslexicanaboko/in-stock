namespace InStock.Lib.Entities.Results
{
  public class ExitPositionWithYieldResult
  {
    public ExitPositionWithYieldResult(
      decimal desiredYield,
      decimal theoreticalPrice,
      decimal theoreticalValue,
      GainResult theoreticalGain,
      decimal currentPrice,
      decimal currentValue,
      GainResult currentGain)
    {
      DesiredYield = desiredYield;
      TheoreticalPrice = theoreticalPrice;
      TheoreticalValue = theoreticalValue;
      TheoreticalGain = theoreticalGain;
      CurrentPrice = currentPrice;
      CurrentValue = currentValue;
      CurrentGain = currentGain;
    }

    public decimal DesiredYield { get; }

    public decimal TheoreticalValue { get; }
    
    public decimal TheoreticalPrice { get; }

    public GainResult TheoreticalGain { get; }

    public decimal CurrentValue { get; }

    public decimal CurrentPrice { get; }

    public GainResult CurrentGain { get; }
  }
}
