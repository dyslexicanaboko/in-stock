using InStock.Lib.Entities.Results;

namespace InStock.Lib.Services
{
  public class TradeAnalysisService
    : ITradeAnalysisService
  {
    private readonly IPositionCalculator _positionCalculator;

    private readonly IPositionService _positionService;

    public TradeAnalysisService(
      IPositionService positionService,
      IPositionCalculator positionCalculator)
    {
      _positionService = positionService;
      _positionCalculator = positionCalculator;
    }

    public async Task<List<CoverPositionLossResult>> CoverPositionLosses(
      int userId,
      string symbol,
      decimal desiredSalesPrice,
      int multipliers = 1)
    {
      var positions = await _positionService.GetCalculatedPositions(userId, symbol);

      var badPositions = positions
        .Where(x => x.TotalGain < 0)
        .ToList();

      var currentPrice = positions.First().CurrentPrice;
      var totalShares = positions.Sum(x => x.Shares);
      var totalLoss = badPositions.Sum(x => x.TotalGain); // Total Loss is already a negative number 
      var totalBadShares = badPositions.Sum(x => x.Shares);
      var totalCostBasis = positions.Sum(x => x.CostBasis);

      var lst = new List<CoverPositionLossResult>(multipliers);

      for (var i = 1; i <= multipliers; i++)
      {
        var proposedShares = i * totalShares;

        var projectedSale = _positionCalculator.CostBasis(desiredSalesPrice, proposedShares);

        //var profit = CalculateProfit(
        //  projectedSale,
        //  totalBadShares,
        //  currentPrice,
        //  totalLoss);

        var projectedGain = _positionCalculator.Gain(projectedSale, totalCostBasis);

        var projectedGainPercentage = _positionCalculator.GainPercentage(projectedGain, totalCostBasis);

        lst.Add(
          new CoverPositionLossResult(
            desiredSalesPrice,
            i,
            proposedShares,
            totalBadShares,
            currentPrice,
            totalLoss,
            projectedGain,
            projectedGainPercentage));
      }

      return lst;
    }

    private static decimal CalculateProfit(
      decimal projectedSale,
      decimal totalBadShares,
      decimal currentPrice,
      decimal totalLoss)
      => projectedSale - totalBadShares * currentPrice + totalLoss;
  }
}
