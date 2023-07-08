using InStock.Lib;
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
    //REMINDER: You cannot access the HttpContext in the constructor so don't try it will be null.

    private int _userId;

    protected int UserId
    {
      get
      {
        if (_userId == 0)
        {
          _userId = GetUserId();
        }

        return _userId;
      }
    }

    protected int GetUserId()
    {
      if (!Request.Headers.TryGetValue("Authorization", out var headerAuth)) 
        throw Lib.Exceptions.Unauthorized.NotAuthenticated();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
      var token = headerAuth
        .First()
        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
#pragma warning restore CS8602 // Dereference of a possibly null reference.

      var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

      var claim = jwt.Claims.Single(x => x.Type == Constants.ClaimsUserId);

      return Convert.ToInt32(claim.Value);
    }
  }
}
