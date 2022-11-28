namespace InStock.Lib.Entities
{

    public class UserEntity : IUser
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime CreateOnUtc { get; set; }
    }
}
