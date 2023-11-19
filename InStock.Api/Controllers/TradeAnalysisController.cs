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

    // GET api/positions/MSFT/coverLosses/45.00?multipliers=3
    [HttpGet("coverLosses/{desiredPrice}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IPositionComposite>))]
    public async Task<ActionResult<IList<CoverPositionLossResult>>> CoverLosses(string symbol, decimal desiredPrice, [FromQuery] int? multipliers)
    {
      var lst = await _service.CoverPositionLosses(UserId, symbol, desiredPrice, multipliers ?? 1);

      return Ok(lst);
    }

    // GET api/positions/MSFT/coverLosses/45.00?multipliers=3
    [HttpGet("exitWithYield/{desiredYield}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IPositionComposite>))]
    public async Task<ActionResult<IList<CoverPositionLossResult>>> CoverLosses(string symbol, decimal desiredYield)
    {
      var proposal = await _service.ExitPositionWithYield(UserId, symbol, desiredYield);

      return Ok(proposal);
    }
  }
}
