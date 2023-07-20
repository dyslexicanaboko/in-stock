using InStock.Lib;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InStock.Api.Controllers
{
  [Route("token")]
  [ApiController]
  public class TokenController : Controller
  {
    private readonly IConfiguration _configuration;
    private readonly IUserService _service;

    public TokenController(IConfiguration config, IUserService service)
    {
      _configuration = config;
      _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserV1GetModel? model)
    {
      if (model is not { Username: { }, Password: { } }) return BadRequest();

      var user = _service.Authenticate(model.Username, model.Password);

      //create claims details based on the user information
      var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        new Claim(Constants.ClaimsUserId, user.UserId.ToString()),
        new Claim("Name", user.Name),
        new Claim("Username", user.Username),
        //new Claim("Email", user.Email)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken(
        _configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"],
        claims,
        expires: DateTime.UtcNow.AddMinutes(10),
        signingCredentials: signIn);

      return await Task.FromResult(Ok(new JwtSecurityTokenHandler().WriteToken(token)));
    }
  }
}
