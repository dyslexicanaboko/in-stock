using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/earnings")]
    [ApiController]
    public class EarningsController
        : BaseApiStarterController<IEarnings, EarningsEntity, EarningsModel>
    {
        public EarningsController(
            IRepository<EarningsEntity> repository,
            IMapper<IEarnings, EarningsEntity, EarningsModel> mapper)
            : base(repository, mapper)
        {

        }
    }
}
