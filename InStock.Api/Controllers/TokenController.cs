using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
  [Route("token")]
  [ApiController]
  public class TokenController : Controller
  {
    private readonly ITokenService _service;

    public TokenController(ITokenService service)
    {
      _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserV1PostModel? model)
    {
      if (model is not { Username: { }, Password: { } }) return BadRequest();

      var token = _service.GetToken(model, GetIpAddress());

      return await Task.FromResult(Ok(token));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Post(RefreshTokenV1PostModel? model)
    {
      if (model is not { Token: { } }) return BadRequest();

      var token = _service.GetToken(model, GetIpAddress());

      return await Task.FromResult(Ok(token));
    }

    private string GetIpAddress()
    {
      if (Request.Headers.ContainsKey("X-Forwarded-For")) return Convert.ToString(Request.Headers["X-Forwarded-For"]);

      var ip = HttpContext.Connection.RemoteIpAddress;

      return ip == null ? string.Empty : ip.MapToIPv4().ToString();
    }
  }
}
