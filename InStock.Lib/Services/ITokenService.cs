using InStock.Lib.Models.Client;

namespace InStock.Lib.Services;

public interface ITokenService
{
  string GetToken(UserV1PostModel model, string ipAddress);

  string GetToken(RefreshTokenV1PostModel model, string ipAddress);
}
