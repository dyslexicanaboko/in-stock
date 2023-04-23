using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Entities
{

    public class UserEntity : IUser
    {
        public UserEntity()
        {

        }

        public UserEntity(IUser target)
        {
            UserId = target.UserId;

            Name = target.Name;
        }

        public UserEntity(UserModel model)
        {
            UserId = model.UserId;
            Name = model.Name;
            CreateOnUtc = model.CreateOnUtc;
        }

        public UserEntity(UserV1CreateModel target)
        {
            Name = target.Name;
        }
        
        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
