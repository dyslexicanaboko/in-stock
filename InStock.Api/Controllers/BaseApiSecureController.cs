using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace InStock.Api.Controllers
{
  [Authorize]
  [ApiController]
  public abstract class BaseApiSecureController
    : ControllerBase
  {
    protected int UserId = 1; //TODO: This needs to be set correctly 0x202306232308

    protected BaseApiSecureController()
    {
      //TODO: Throw exception if userId not found?
      if (!HttpContext.Request.Headers.TryGetValue("Authorization", out var headerAuth)) return;

      var token = headerAuth.First().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

      var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
      
      UserId = Convert.ToInt32(jwt.Claims.Single(x => x.Value == "UserId"));
    }
  }
}
