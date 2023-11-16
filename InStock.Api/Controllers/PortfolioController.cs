using InStock.Lib.Entities.Composites;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
  [Route("api/portfolios")]
  [ApiController]
  public class PortfolioController
    : BaseApiSecureController
  {
    private readonly IPortfolioService _service;

    public PortfolioController(
      IPortfolioService service)
    {
      _service = service;
    }

    // GET api/portfolios/5
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPortfolioComposite))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public async Task<ActionResult<IPortfolioComposite>> Get(int userId)
    {
      if (UserId != userId) throw Lib.Exceptions.Forbidden.AccessDenied();

      var composite = await _service.GetPortfolio(userId);

      return Ok(composite);
    }

    // GET api/portfolios/5/stock/7
    [HttpGet("{userId}/stock/{stockId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPortfolioComposite))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public async Task<ActionResult<IPortfolioComposite>> Get(int userId, int stockId)
    {
      if (UserId != userId) throw Lib.Exceptions.Forbidden.AccessDenied();

      var composite = await _service.GetPortfolio(userId, stockId);

      return Ok(composite);
    }
  }
}
