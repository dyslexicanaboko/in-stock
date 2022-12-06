namespace InStock.Lib.Entities
{
    public interface IUser
    {
        int UserId { get; set; }

        string Name { get; set; }

        DateTime CreateOnUtc { get; set; }
    }
}
