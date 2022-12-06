using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/trades")]
    [ApiController]
    public class TradeController
        : BaseApiController<ITrade, TradeEntity, TradeModel>
    {
        public TradeController(
            IRepository<TradeEntity> repository,
            IMapper<ITrade, TradeEntity, TradeModel> mapper)
            : base(repository, mapper)
        {

        }
    }
}
