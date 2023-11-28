using InStock.Lib.Entities.Composites;
using InStock.Lib.Entities.Results;
using InStock.Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
  [Route("api/positions/{symbol}")]
  [ApiController]
  public class TradeAnalysisController 
    : BaseApiSecureController
  {
    private readonly ITradeAnalysisService _service;

    public TradeAnalysisController(
      ITradeAnalysisService service)
    {
      _service = service;
    }

    // GET api/positions/MSFT/coverLosses/45.00?proposals=3
    [HttpGet("coverLosses/{desiredPrice}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CoverPositionLossResult))]
    public async Task<ActionResult<CoverPositionLossResult>> CoverLosses(string symbol, decimal desiredPrice, [FromQuery] int? proposals)
    {
      var result = await _service.CoverPositionLosses(UserId, symbol, desiredPrice, proposals ?? 1);

      return Ok(result);
    }

    // GET api/positions/MSFT/exitWithYield/5000.00
    [HttpGet("exitWithYield/{desiredYield}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExitPositionWithYieldResult))]
    public async Task<ActionResult<ExitPositionWithYieldResult>> ExitWithYield(string symbol, decimal desiredYield)
    {
      var proposal = await _service.ExitPositionWithYield(UserId, symbol, desiredYield);

      return Ok(proposal);
    }
  }
}
