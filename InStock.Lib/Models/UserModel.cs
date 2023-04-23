using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class UserModel : IUser
    {
        public UserModel(IUser target)
        {
            UserId = target.UserId;
            Name = target.Name;
        }

        public UserModel(UserEntity entity)
        {
            UserId = entity.UserId;
            Name = entity.Name;
            CreateOnUtc = entity.CreateOnUtc;
        }

        public int UserId { get; }

        public string Name { get; }

        public DateTime CreateOnUtc { get; }
    }
}
