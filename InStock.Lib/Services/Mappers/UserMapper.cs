using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public class UserMapper
        : MapperBase, IMapper<IUser, UserEntity, UserModel>, IUserMapper
    {
        public UserEntity? ToEntity(UserModel? model)
        {
            if (model == null) return null;

            var entity = new UserEntity();
            entity.UserId = model.UserId;
            entity.Name = model.Name;
            entity.CreateOnUtc = model.CreateOnUtc;

            return entity;
        }

        public UserModel? ToModel(UserEntity? entity)
        {
            if (entity == null) return null;

            var model = new UserModel();
            model.UserId = entity.UserId;
            model.Name = entity.Name;
            model.CreateOnUtc = entity.CreateOnUtc;

            return model;
        }

        public UserEntity? ToEntity(IUser? target)
        {
            if (target == null) return null;

            var entity = new UserEntity();
            entity.UserId = target.UserId;
            entity.Name = target.Name;

            return entity;
        }

        public UserModel? ToModel(IUser? target)
        {
            if (target == null) return null;

            var model = new UserModel();
            model.UserId = target.UserId;
            model.Name = target.Name;

            return model;
        }

        public UserEntity? ToEntity(UserV1CreateModel? target)
        {
            if (target == null) return null;

            var model = new UserEntity();
            model.Name = target.Name;

            return model;
        }

        public UserV1CreatedModel? ToCreatedModel(IUser? target)
        {
            if (target == null) return null;
            
            var model = new UserV1CreatedModel();
            model.UserId = target.UserId;
            model.Name = target.Name;

            return model;
        }

        public IList<UserModel> ToModel(IList<UserEntity> target) => ToList(target, ToModel);
    }
}
