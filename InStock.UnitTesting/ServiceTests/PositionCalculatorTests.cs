using FakeItEasy;
using InStock.Lib.Services;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace InStock.UnitTesting.ServiceTests
{
  public class PositionCalculatorTests
    : TestBaseInStock
  {
    [SetUp]
    public void Setup()
    {
      var quote = GetSomeQuote();
      quote.Price = 32.45;

      _quoteService = A.Fake<IQuoteService>();
      
      A.CallTo(() => _quoteService.Add(A<int>._, A<string>._)).Returns(quote);

      _sut = new PositionCalculator(NullLogger<PositionCalculator>.Instance, _quoteService);
    }

    private IQuoteService _quoteService;

    private IPositionCalculator _sut;

    [Test]
    public void SetCalculableProperties_WhenPositivePosition_PositivePercentageGain()
    {
      var actual = Math.Round(_sut.GainPercentage(50M, 100M), 2);

      Assert.Greater(actual, 0);
      Assert.AreEqual(0.5, actual);
    }

    [Test]
    public void SetCalculableProperties_WhenNegativePosition_NegativePercentageGain()
    {
      var actual = Math.Round(_sut.GainPercentage(-136.50M, 1_110.00M), 2);

      Assert.Less(actual, 0);
      Assert.AreEqual(-0.12, actual);
    }
  }
}
