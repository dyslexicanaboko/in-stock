using InStock.Lib.Models.Client;

namespace InStock.Lib.Entities
{

    public class UserEntity : IUser
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UserEntity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public UserEntity(IUser target)
        {
            UserId = target.UserId;

            Name = target.Name;
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
