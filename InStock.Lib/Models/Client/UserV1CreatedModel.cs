using InStock.Lib.Entities;

namespace InStock.Lib.Models.Client
{
    public class UserV1CreatedModel
    {
        public UserV1CreatedModel(IUser target)
        {
            UserId = target.UserId;

            Name = target.Name;
        }
        
        public int UserId { get; set; }
        
        public string Name { get; set; }
    }
}
