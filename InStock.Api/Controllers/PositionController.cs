using CommunityToolkit.Diagnostics;
using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class PositionController
        : BaseApiSecureController
    {
        private readonly IPositionService _service;
        private readonly IPositionMapper _mapper;

        public PositionController(
            IPositionService service,
            IPositionMapper mapper)
        {
            _service = service;

            _mapper = mapper;
        }

        // GET api/position/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPosition))]
        public ActionResult<IPosition> Get(int id)
        {
            var entity = _service.GetPosition(id);

            return Ok(_mapper.ToModel(entity));
        }

        // GET api/position/MSFT/symbol
        [HttpGet("{symbol}/symbol")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IPosition>))]
        public ActionResult<IList<IPosition>> Get(string symbol)
        {
            //TODO: Need to get the UserId from the header or something?
            var lst = _service.GetPosition(UserId, symbol);

            return Ok(_mapper.ToModel(lst));
        }

        // POST api/position
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IPosition))]
        public async Task<ActionResult<PositionModel>> Post([FromBody] PositionV1CreateModel model)
        {
            var entity = _mapper.ToEntity(model);
            
            Guard.IsNotNull(entity);

            //TODO: Need to get the UserId from the header or something?
            entity.UserId = UserId;

            var lst = new List<PositionEntity> { entity };

            var result = (await Task.FromResult(_service.Add(lst))).Single();

            if (!result.IsSuccessful) throw result.Exception!;

            var m = _mapper.ToModel(result.Position);

            return CreatedAtAction(nameof(Get), new { id = m!.PositionId }, m);
        }

        // POST api/position/multiple
        [HttpPost("multiple")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PositionV1CreateMultipleModel))]
        public async Task<ActionResult<PositionV1CreateMultipleModel>> Post([FromBody] PositionV1CreateModel[] model)
        {
            var entity = _mapper.ToEntity(model);

            Guard.IsNotNull(entity);
            Guard.IsNotEmpty(entity);

            var lst = entity.ToList();

            //TODO: Need to get the UserId from the header or something?
            lst.ForEach(x => x.UserId = UserId);

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

        // DELETE api/position/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);

            return NoContent();
        }

        // DELETE api/position/MSFT/symbol
        [HttpDelete("{symbol}/symbol")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(string symbol)
        {
            //TODO: Need to get the UserId from the header or something?
            _service.Delete(UserId, symbol);

            return NoContent();
        }
    }
}
