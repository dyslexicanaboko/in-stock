using CommunityToolkit.Diagnostics;
using InStock.Lib.Entities;
using InStock.Lib.Models;
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
        [HttpGet("{id:int}")]
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
        public async Task<ActionResult<EarningsModel>> Post([FromBody] EarningsV1CreateModel model)
        {
            var entity = _mapper.ToEntity(model);

            Guard.IsNotNull(entity);

            var lst = new List<EarningsEntity> { entity };

            var result = (await Task.FromResult(_service.Add(lst))).Single();

            if (!result.IsSuccessful) throw result.Exception!;

            var m = _mapper.ToModel(result.Earnings);

            return CreatedAtAction(nameof(Get), new { id = m!.EarningsId }, m);
        }

        // POST api/position/multiple
        [HttpPost("multiple")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PositionV1CreateMultipleModel))]
        public async Task<ActionResult<EarningsV1CreateMultipleModel>> Post([FromBody] EarningsV1CreateModel[] model)
        {
            var entity = _mapper.ToEntity(model);

            Guard.IsNotNull(entity);
            Guard.IsNotEmpty(entity);

            var lst = entity.ToList();

            var results = await Task.FromResult(_service.Add(lst));

            var m = _service.TranslateToModel(results);

            //Ignoring the URI for this because this doesn't conform to rigid REST standards
            //If there is at least one success then return a 201
            if (m.Success.Any()) return Created(string.Empty, m);

            //If there are errors only then raise a bad request
            if (m.Failure.Any()) return BadRequest(m);

            //If there is nothing then a 200 is fine
            return Ok(m);
        }

        // DELETE api/earnings/5
        [HttpDelete("{id:int}")]
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
