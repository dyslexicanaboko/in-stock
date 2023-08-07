using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Models.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InStock.Lib.Services
{
  public class TokenService
    : ITokenService
  {
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    //TODO: Relocate the UserService methods to this class instead
    public TokenService(IConfiguration config, IUserService userService, IRefreshTokenRepository refreshTokenRepository)
    {
      _configuration = config;
      _userService = userService;
      _refreshTokenRepository = refreshTokenRepository;
    }

    public string GetToken(UserV1PostModel model, string ipAddress)
    {
      var user = _userService.Authenticate(model.Username, model.Password);

      return GetToken(user, ipAddress);
    }

    public string GetToken(RefreshTokenV1PostModel model, string ipAddress)
    {
      //TODO: Hit the DB one time - lookup user by token
      var refreshToken = _refreshTokenRepository.Select(model.Token);

      //TODO: If the token is not found, an error must be thrown
      var user = _userService.GetUser(refreshToken.UserId);

      //TODO: If the user is not found, an error must be thrown
      _refreshTokenRepository.Delete(user.UserId, refreshToken.Token);

      return GetToken(user, ipAddress);
    }

    private string GetToken(UserEntity user, string ipAddress)
    {
      var utcNow = DateTime.UtcNow;

      var refreshToken = GenerateRefreshToken(user.UserId, utcNow, ipAddress);

      _refreshTokenRepository.DeleteExpired(user.UserId);
      _refreshTokenRepository.Insert(refreshToken);

      //create claims details based on the user information
      var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),
        new Claim(Constants.RefreshToken, refreshToken.Token),
        new Claim(Constants.ClaimsUserId, user.UserId.ToString()),
        new Claim(Constants.Name, user.Name),
        new Claim(Constants.Username, user.Username),
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken(
        _configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"],
        claims,
        expires: utcNow.AddMinutes(10),
        signingCredentials: signIn);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //https://github.com/cornflourblue/dotnet-6-jwt-refresh-tokens-api
    private string GetUniqueToken()
    {
      //TODO: Run away train situation?
      while (true)
      {
        // token is a cryptographically strong random sequence of values
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        // ensure token is unique by checking against db
        if (_refreshTokenRepository.Select(token) != null) continue;

        return token;
      }
    }

    private RefreshTokenEntity GenerateRefreshToken(int userId, DateTime utcNow, string ipAddress)
    {
      var refreshToken = new RefreshTokenEntity
      {
        RefreshTokenId = Guid.NewGuid(),
        UserId = userId,
        Token = GetUniqueToken(),
        CreatedOn = utcNow,
        ExpiresOn = utcNow.AddDays(7),
        CreatedByIp = ipAddress
      };

      return refreshToken;
    }
  }
}
