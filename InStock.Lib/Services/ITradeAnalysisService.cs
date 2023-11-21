using InStock.Lib.Entities.Results;

namespace InStock.Lib.Services;

public interface ITradeAnalysisService
{
  Task<CoverPositionLossResult> CoverPositionLosses(
    int userId,
    string symbol,
    decimal desiredSalesPrice,
    int proposals = 1);

  Task<ExitPositionWithYieldResult> ExitPositionWithYield(
    int userId,
    string symbol,
    decimal desiredYield);
}
