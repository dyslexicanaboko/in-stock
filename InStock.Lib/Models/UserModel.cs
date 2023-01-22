using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class UserModel : IUser
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UserModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public UserModel(IUser target)
        {
            UserId = target.UserId;

            Name = target.Name;
        }
        
        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
