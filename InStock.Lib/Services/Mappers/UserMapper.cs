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

            var entity = new UserEntity(model);
            entity.CreateOnUtc = model.CreateOnUtc;

            return entity;
        }

        public UserModel? ToModel(UserEntity? entity)
        {
            if (entity == null) return null;

            var model = new UserModel(entity);
            model.CreateOnUtc = entity.CreateOnUtc;

            return model;
        }

        public UserEntity? ToEntity(IUser? target)
        {
            if (target == null) return null;

            var entity = new UserEntity(target);

            return entity;
        }

        public UserModel? ToModel(IUser? target)
        {
            if (target == null) return null;

            var model = new UserModel(target);

            return model;
        }

        public UserEntity? ToEntity(UserV1CreateModel? target)
        {
            if (target == null) return null;

            var model = new UserEntity(target);

            return model;
        }

        public UserV1CreatedModel? ToCreatedModel(IUser? target)
        {
            if (target == null) return null;
            
            var model = new UserV1CreatedModel(target);

            return model;
        }

        public IList<UserModel> ToModel(IList<UserEntity> target) => ToList(target, ToModel);
    }
}
