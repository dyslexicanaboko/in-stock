using InStock.Lib.Entities.Results;

namespace InStock.Lib.Services;

public interface ITradeAnalysisService
{
  Task<List<CoverPositionLossResult>> CoverPositionLosses(
    int userId,
    string symbol,
    decimal desiredSalesPrice,
    int multipliers = 1);

  Task<ExitPositionWithYieldResult> ExitPositionWithYield(
    int userId,
    string symbol,
    decimal desiredYield);
}
