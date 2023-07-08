using InStock.Lib.Entities;

namespace InStock.Lib.DataAccess
{
    public interface IUserRepository
        : IRepository<UserEntity>
    {
      UserEntity? Select(string username);
    }
}
