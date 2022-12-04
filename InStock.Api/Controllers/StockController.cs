using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController
        : BaseApiController<IStock, StockEntity, StockModel>
    {
        public StockController(
            IRepository<StockEntity> repository,
            IMapper<IStock, StockEntity, StockModel> mapper)
            : base(repository, mapper)
        {

        }
    }
}
