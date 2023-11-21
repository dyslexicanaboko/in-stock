using InStock.Lib.Entities.Results;
using InStock.Lib.Services.Factory;

namespace InStock.Lib.Services
{
  public class TradeAnalysisService
    : ITradeAnalysisService
  {
    private readonly IGainFactory _gainFactory;

    private readonly IPositionCalculator _positionCalculator;

    private readonly IPositionService _positionService;

    public TradeAnalysisService(
      IPositionService positionService,
      IPositionCalculator positionCalculator,
      IGainFactory gainFactory)
    {
      _positionService = positionService;
      _positionCalculator = positionCalculator;
      _gainFactory = gainFactory;
    }

    public async Task<CoverPositionLossResult> CoverPositionLosses(
      int userId,
      string symbol,
      decimal desiredSalesPrice,
      int proposals = 1)
    {
      var positions = await _positionService.GetCalculatedPositions(userId, symbol);

      var badPositions = positions
        .Where(x => x.TotalGain < 0)
        .ToList();

      var currentPrice = positions.First().CurrentPrice;
      var totalShares = positions.Sum(x => x.Shares);
      var currentLoss = badPositions.Sum(x => x.TotalGain); // Total Loss is already a negative number 
      var totalBadShares = badPositions.Sum(x => x.Shares);
      var totalCostBasis = positions.Sum(x => x.CostBasis);

      var result = new CoverPositionLossResult(
        desiredSalesPrice,
        totalShares,
        totalBadShares,
        currentPrice,
        currentLoss);

      for (var p = 1; p <= proposals; p++)
      {
        var proposedShares = _positionCalculator.Round(p * totalBadShares);

        var projectedSale = _positionCalculator.CostBasis(desiredSalesPrice, proposedShares + totalShares);

        var projectedGain = _gainFactory.Create(projectedSale, totalCostBasis);

        result.Proposals.Add(
          new CoverPositionLossProposal(
            p,
            proposedShares,
            proposedShares * currentPrice,
            projectedSale,
            projectedGain));
      }

      return result;
    }

    public async Task<ExitPositionWithYieldResult> ExitPositionWithYield(
      int userId,
      string symbol,
      decimal desiredYield)
    {
      var positions = await _positionService.GetCalculatedPositions(userId, symbol);

      var shares = positions.Sum(x => x.Shares);
      var currentValue = positions.Sum(x => x.CurrentValue);
      var costBasis = positions.Sum(x => x.CostBasis);
      var currentGain = _gainFactory.Create(currentValue, costBasis);

      var theoreticalValue = _positionCalculator.TheoreticalValue(desiredYield, costBasis);
      var theoreticalPrice = _positionCalculator.TheoreticalPrice(theoreticalValue, shares);
      var theoreticalGain = _gainFactory.Create(theoreticalValue, costBasis);

      return new ExitPositionWithYieldResult(
        desiredYield,
        theoreticalPrice,
        theoreticalValue,
        theoreticalGain,
        positions.First().CurrentPrice,
        currentValue,
        currentGain);
    }

    private static decimal CalculateProfit(
      decimal projectedSale,
      decimal totalBadShares,
      decimal currentPrice,
      decimal totalLoss)
      => projectedSale - totalBadShares * currentPrice + totalLoss;
  }
}
