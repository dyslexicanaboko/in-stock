using FakeItEasy;
using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Entities.Results;
using InStock.Lib.Services;
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

      _sut = new TradeAnalysisService(positionService, calculator);
    }

    private ITradeAnalysisService _sut;

    private readonly List<PositionEntity> _positions = new List<PositionEntity>
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

    /*
     * The goal here was to cover the losses.
     * I don't know that the stock will get back to 85, it did brush 82 in the past year (2023).
     * Calculating the current position based on the 11/07 price, the position's value is -582.
     * The strategy I am using right now is the total quantity that's under water and a multiplier.
     * So it's 33 shares, upping it to 50 or 66 whatever I can afford.
     * So that's roughly a multiplier of 2.
     * I think I can sell for 82.
     * Right now the stock is half off, so I need to take advantage of this to wait for the upswing.
     * I am not sure if it will go lower, but this is already a 52 week low.
     */
    [Test]
    public async Task CoverPositionLosses_WhenStockIsUnderWater_ThenThreeProposalsAreOffered()
    {
      //Arrange
      var multipliers = 1;
      var desiredSalesPrice = 45M;

      var expected = new CoverPositionLossResult(
        desiredSalesPrice,
        1,
        50,
        50,
        30.81M,
        -357.24M,
        352.26M,
        0.1856M);

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
      Assert.AreEqual(expected.ProjectedGain, actual.ProjectedGain);
      Assert.AreEqual((double)expected.ProjectedGainPercentage, (double)actual.ProjectedGainPercentage, 0.01D);
    }
  }
}
