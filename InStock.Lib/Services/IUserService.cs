using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public interface IUserService
    {
        UserEntity Add(UserEntity user);
        IList<UserEntity> GetAllUsers();
        UserEntity GetUser(int id);
    }
}