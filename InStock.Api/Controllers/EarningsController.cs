using InStock.Lib.Entities;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/earnings")]
    [ApiController]
    public class EarningsController
        : BaseApiSecureController
    {
        private readonly IEarningsService _service;
        private readonly IEarningsMapper _mapper;

        public EarningsController(
            IEarningsService service,
            IEarningsMapper mapper)
        {
            _service = service;
            
            _mapper = mapper;
        }

        // GET api/earnings/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEarnings))]
        public ActionResult<IEarnings> Get(int id)
        {
            var entity = _service.GetEarnings(id);

            return Ok(_mapper.ToModel(entity));
        }

        // GET api/earnings/MSFT/symbol
        [HttpGet("{symbol}/symbol")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IEarnings>))]
        public ActionResult<IList<IEarnings>> Get(string symbol)
        {
            var lst = _service.GetEarnings(symbol);

            return Ok(_mapper.ToModel(lst));
        }

        // POST api/earnings
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEarnings))]
        public async Task<ActionResult<EarningsV1CreatedModel>> Post([FromBody] EarningsV1CreateModel model)
        {
            var entity = await Task.FromResult(_service.Add(_mapper.ToEntity(model)));

            var m = _mapper.ToCreatedModel(entity);

            return CreatedAtAction(nameof(Get), new { id = m.EarningsId }, m);
        }

        // DELETE api/earnings/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
         
            return NoContent();
        }

        // DELETE api/earnings/MSFT/symbol
        [HttpDelete("{symbol}/symbol")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(string symbol)
        {
            _service.Delete(symbol);

            return NoContent();
        }
    }
}
