using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;

namespace InStock.Lib.Services.Mappers
{
    public interface IUserMapper
    {
        UserV1CreatedModel ToCreatedModel(IUser target);

        UserEntity ToEntity(IUser target);

        UserEntity ToEntity(UserModel model);

        UserModel ToModel(IUser target);

        UserModel ToModel(UserEntity entity);

        UserEntity ToEntity(UserV1CreateModel target);

        IList<UserModel> ToModel(IList<UserEntity> target);
    }
}