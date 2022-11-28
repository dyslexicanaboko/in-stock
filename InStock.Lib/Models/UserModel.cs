using InStock.Lib.Entities;

namespace InStock.Lib.Models
{
    public class UserModel : IUser
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
