namespace InStock.Lib.Entities.Results
{
  public class GainResult
  {
    public GainResult(
      decimal gain,
      decimal gainRate)
    {
      Gain = gain;
      GainRate = gainRate;
    }

    public decimal Gain { get; }

    public decimal GainRate { get; }
  }
}
