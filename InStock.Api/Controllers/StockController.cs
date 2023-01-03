using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController
        : BaseApiSecureController
    {
        private readonly IStockService _service;
        private readonly IMapper<IStock, StockEntity, StockModel> _mapper;

        public StockController(
            IStockService service,
            IMapper<IStock, StockEntity, StockModel> mapper)
        {
            _service = service;

            _mapper = mapper;
        }

        // GET api/stock/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IStock))]
        public ActionResult<IStock> Get(int id)
        {
            var entity = _service.GetStock(id);

            return Ok(_mapper.ToModel(entity));
        }

        // POST api/stock
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IStock))]
        public async Task<ActionResult<IStock>> Post([FromBody] StockV1Model model)
        {
            var entity = await _service.Add(model.Symbol);

            var m = _mapper.ToModel(entity);

            return CreatedAtAction(nameof(Get), new { id = m.StockId }, m);
        }
    }
}
