namespace InStock.Lib.Entities.Results
{
  public class GainResult
  {
    public GainResult(
      decimal gain,
      decimal gainPercentage)
    {
      Gain = gain;
      GainPercentage = gainPercentage;
    }

    public decimal Gain { get; }

    public decimal GainPercentage { get; }
  }
}
