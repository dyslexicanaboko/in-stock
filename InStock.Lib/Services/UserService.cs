using InStock.Lib.DataAccess;
using InStock.Lib.Entities;

namespace InStock.Lib.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repoUser;

        public UserService(
            IUserRepository repoUser)
        {
            _repoUser = repoUser;
        }

        public UserEntity? GetUser(int id)
        {
            var dbEntity = _repoUser.Using(x => x.Select(id));

            return dbEntity;
        }

        public IList<UserEntity> GetAllUsers()
        {
            var lst = _repoUser.Using(x => x.SelectAll()).ToList();

            return lst;
        }

        public UserEntity Add(UserEntity user)
        {
            user.UserId = _repoUser.Using(x => x.Insert(user));

            return user;
        }
    }
}
