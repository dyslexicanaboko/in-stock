using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess;

public interface IRefreshTokenRepository 
  : IRepository
{
  IEnumerable<RefreshTokenEntity> SelectByUserId(int userId);

  RefreshTokenEntity? Select(string token);

  void Insert(RefreshTokenEntity entity);

  void Delete(int userId, string token);

  void DeleteExpired(int userId);
}
