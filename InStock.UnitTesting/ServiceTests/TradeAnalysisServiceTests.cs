using FakeItEasy;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Entities.Results;
using InStock.Lib.Services;
using InStock.Lib.Services.Factory;
using InStock.Lib.Validation;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace InStock.UnitTesting.ServiceTests
{
  [TestFixture]
  public class TradeAnalysisServiceTests
    : TestBaseInStock
  {
    [SetUp]
    public void Setup()
    {
      var dtmService = new DateTimeService();
      var repoStock = A.Fake<IStockRepository>();
      var validation = new PositionValidation();

      var repoPosition = A.Fake<IPositionRepository>();

      A.CallTo(() => repoPosition.Select(A<int>._, A<string>._))
        .Returns(_positions);

      var quoteService = A.Fake<IQuoteService>();

      A.CallTo(() => quoteService.Add(A<int>._, A<string>._))
        .Returns(
          new QuoteEntity
          {
            QuoteId = 2419,
            StockId = 8,
            Date = DateTime.Parse("11/17/2023 00:00:00"),
            Price = 30.81,
            Volume = 676364,
            CreatedOnUtc = DateTime.Parse("11/17/2023 23:11:43"),
          }
        );

      var calculator = new PositionCalculator(NullLogger<PositionCalculator>.Instance, quoteService);

      var positionService = new PositionService(
        repoPosition,
        repoStock,
        validation,
        dtmService,
        calculator);

      _sut = new TradeAnalysisService(positionService, calculator, new GainFactory(calculator));
    }

    private ITradeAnalysisService _sut;

    private readonly List<PositionEntity> _positions = new()
    {
      new PositionEntity
      {
        PositionId = 17,
        UserId = 1,
        StockId = 8,
        DateOpenedUtc = DateTime.Parse("12/29/2020 05:00:00"),
        DateClosedUtc = null,
        Price = 37.00M,
        Quantity = 30.0000M,
        CreateOnUtc = DateTime.Parse("3/27/2023 04:27:48"),
      },
      new PositionEntity
      {
        PositionId = 16,
        UserId = 1,
        StockId = 8,
        DateOpenedUtc = DateTime.Parse("7/19/2021 04:00:00"),
        DateClosedUtc = null,
        Price = 48.14M,
        Quantity = 2.0000M,
        CreateOnUtc = DateTime.Parse("3/27/2023 04:27:48"),
      },
      new PositionEntity
      {
        PositionId = 15,
        UserId = 1,
        StockId = 8,
        DateOpenedUtc = DateTime.Parse("10/5/2021 04:00:00"),
        DateClosedUtc = null,
        Price = 48.33M,
        Quantity = 2.0000M,
        CreateOnUtc = DateTime.Parse("3/27/2023 04:27:48"),
      },
      new PositionEntity
      {
        PositionId = 14,
        UserId = 1,
        StockId = 8,
        DateOpenedUtc = DateTime.Parse("12/7/2021 05:00:00"),
        DateClosedUtc = null,
        Price = 41.90M,
        Quantity = 2.0000M,
        CreateOnUtc = DateTime.Parse("3/27/2023 04:27:48"),
      },
      new PositionEntity
      {
        PositionId = 13,
        UserId = 1,
        StockId = 8,
        DateOpenedUtc = DateTime.Parse("1/27/2022 05:00:00"),
        DateClosedUtc = null,
        Price = 36.00M,
        Quantity = 4.0000M,
        CreateOnUtc = DateTime.Parse("3/27/2023 04:27:48"),
      },
      new PositionEntity
      {
        PositionId = 12,
        UserId = 1,
        StockId = 8,
        DateOpenedUtc = DateTime.Parse("4/13/2022 04:00:00"),
        DateClosedUtc = null,
        Price = 36.70M,
        Quantity = 10.0000M,
        CreateOnUtc = DateTime.Parse("3/27/2023 04:27:48"),
      }
    };
    
    [Test]
    public async Task CoverPositionLosses_WhenStockIsUnderWater_ThenAProposalIsOffered()
    {
      //Arrange
      const int multipliers = 1;
      const decimal desiredSalesPrice = 45M;

      var expected = new CoverPositionLossResult(
        desiredSalesPrice,
        1,
        50,
        50,
        30.81M,
        -357.24M,
        new GainResult(352.26M, 0.1856M));

      //Act
      var results = await _sut.CoverPositionLosses(
        SomeUserId,
        SomeSymbol,
        desiredSalesPrice,
        multipliers);

      //Assert
      Assert.AreEqual(multipliers, results.Count);

      var actual = results.Single();

      Assert.AreEqual(expected.DesiredSalesPrice, actual.DesiredSalesPrice);
      Assert.AreEqual(expected.Multiplier, actual.Multiplier);
      Assert.AreEqual(expected.ProposedShares, actual.ProposedShares);
      Assert.AreEqual(expected.TotalBadShares, actual.TotalBadShares);
      Assert.AreEqual(expected.CurrentPrice, actual.CurrentPrice);
      Assert.AreEqual(expected.TotalLoss, actual.TotalLoss);
      Assert.AreEqual(expected.ProjectedGain.Gain, actual.ProjectedGain.Gain);
      AssertAreEqual(expected.ProjectedGain.GainPercentage, actual.ProjectedGain.GainPercentage);
    }

    [Test]
    public async Task ExitPositionWithYield_WhenAYieldIsDesired_ThenAProposalIsOfferedMatchingTheYield()
    {
      //Arrange
      const decimal desiredYield = 0.25M;

      var expected = new ExitPositionWithYieldResult(
        desiredYield,
        47.44M,
        2372.18M,
        new GainResult(474.44M, desiredYield),
        30.81M,
        1540.50M,
        new GainResult(-357.24M, -0.188245M));

      //Act
      var results = await _sut.ExitPositionWithYield(
        SomeUserId,
        SomeSymbol,
        desiredYield);

      //Assert
      Assert.AreEqual(expected.DesiredYield, results.DesiredYield);
      AssertAreEqual(expected.TheoreticalPrice, results.TheoreticalPrice);
      AssertAreEqual(expected.TheoreticalValue, results.TheoreticalValue);
      AssertAreEqual(expected.TheoreticalGain.Gain, results.TheoreticalGain.Gain);
      AssertAreEqual(expected.TheoreticalGain.GainPercentage, results.TheoreticalGain.GainPercentage);
      Assert.AreEqual(expected.CurrentPrice, results.CurrentPrice);
      Assert.AreEqual(expected.CurrentValue, results.CurrentValue);
      AssertAreEqual(expected.CurrentGain.Gain, results.CurrentGain.Gain);
      AssertAreEqual(expected.CurrentGain.GainPercentage, results.CurrentGain.GainPercentage);
    }
  }
}
