using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController
        : BaseApiController<IUser, UserEntity, UserModel>
    {
        public UserController(
            IRepository<UserEntity> repository,
            IMapper<IUser, UserEntity, UserModel> mapper)
            : base(repository, mapper)
        {

        }
    }
}
