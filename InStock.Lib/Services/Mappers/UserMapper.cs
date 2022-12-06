using InStock.Lib.Entities;
using InStock.Lib.Models;

namespace InStock.Lib.Services.Mappers
{
    public class UserMapper
        : IMapper<IUser, UserEntity, UserModel>
    {
        public UserEntity ToEntity(UserModel model)
        {
            var entity = new UserEntity();
            entity.UserId = model.UserId;
            entity.Name = model.Name;
            entity.CreateOnUtc = model.CreateOnUtc;

            return entity;
        }

        public UserModel ToModel(UserEntity entity)
        {
            var model = new UserModel();
            model.UserId = entity.UserId;
            model.Name = entity.Name;
            model.CreateOnUtc = entity.CreateOnUtc;

            return model;
        }

        public UserEntity ToEntity(IUser target)
        {
            var entity = new UserEntity();
            entity.UserId = target.UserId;
            entity.Name = target.Name;
            entity.CreateOnUtc = target.CreateOnUtc;

            return entity;
        }

        public UserModel ToModel(IUser target)
        {
            var model = new UserModel();
            model.UserId = target.UserId;
            model.Name = target.Name;
            model.CreateOnUtc = target.CreateOnUtc;

            return model;
        }
    }
}
