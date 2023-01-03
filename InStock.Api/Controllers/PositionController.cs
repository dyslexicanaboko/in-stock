using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class PositionController
        : BaseApiStarterController<IPosition, PositionEntity, PositionModel>
    {
        public PositionController(
            IRepository<PositionEntity> repository,
            IMapper<IPosition, PositionEntity, PositionModel> mapper)
            : base(repository, mapper)
        {

        }
    }
}
