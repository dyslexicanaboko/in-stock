using InStock.Lib.Entities.Results;

namespace InStock.Lib.Services.Factory
{
  public class GainFactory
    : IGainFactory
  {
    private readonly IPositionCalculator _calculator;

    public GainFactory(IPositionCalculator calculator)
    {
      _calculator = calculator;
    }

    public GainResult Create(decimal totalValue, decimal costBasis)
    {
      var gain = _calculator.Gain(totalValue, costBasis);
      var gainPercentage = _calculator.GainPercentage(gain, costBasis);

      return new GainResult(gain, gainPercentage);
    }
  }
}
