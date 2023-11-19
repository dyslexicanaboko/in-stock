using InStock.Lib.Entities;
using InStock.Lib.Entities.Composites;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using InStock.Lib.Validation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
  [Route("api/positions")]
  [ApiController]
  public class PositionController
    : BaseApiSecureController
  {
    private readonly IPositionMapper _mapper;

    private readonly IPositionService _service;

    public PositionController(
      IPositionService service,
      IPositionMapper mapper)
    {
      _service = service;

      _mapper = mapper;
    }

    // GET api/positions/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IPosition))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult<IPosition> Get(int id)
    {
      //TODO: Need to verify that the user has access to the requested resource
      var entity = _service.GetPosition(id); //TODO: UserId needs to be passed

      if (entity == null) throw Lib.Exceptions.NotFound.Position(id);

      return Ok(_mapper.ToModel(entity));
    }

    // GET api/positions/MSFT/symbol
    [HttpGet("{symbol}/symbol")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IPosition>))]
    public ActionResult<IList<IPosition>> Get(string symbol)
    {
      var lst = _service.GetPositions(UserId, symbol);

      return Ok(_mapper.ToModel(lst));
    }

    //TODO: Not sure how to say - this is positions, but calculated - this feels wrong
    // GET api/positions/MSFT/symbolCalculated
    [HttpGet("{symbol}/symbolCalculated")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IPositionComposite>))]
    public async Task<ActionResult<IList<IPositionComposite>>> GetCalculated(string symbol)
    {
      var lst = await _service.GetCalculatedPositions(UserId, symbol);

      return Ok(_mapper.ToCalculatedModel(lst));
    }
    
    // POST api/positions
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IPosition))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<ActionResult<PositionModel>> Post([FromBody] PositionV1CreateModel model)
    {
      var entity = _mapper.ToEntity(UserId, model);

      Validations.IsNotNull(entity, nameof(model));

      var lst = new List<PositionEntity> { entity };

      var result = (await Task.FromResult(_service.Add(lst))).Single();

      if (!result.IsSuccessful) throw result.Exception!;

      var m = _mapper.ToModel(result.Position);

      return CreatedAtAction(nameof(Get), new { id = m!.PositionId }, m);
    }

    // POST api/positions/multiple
    [HttpPost("multiple")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PositionV1CreateMultipleModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<ActionResult<PositionV1CreateMultipleModel>> Post([FromBody] PositionV1CreateModel[] models)
    {
      
      var entities = _mapper.ToEntity(UserId, models);

      Validations.IsNotNull(entities, nameof(models));
      Validations.IsNotEmpty(entities, nameof(models));

      var lst = entities.ToList();

      var results = await Task.FromResult(_service.Add(lst));

      var m = _mapper.ToModel(results);

      //Ignoring the URI for this because this doesn't conform to rigid REST standards
      //If there is at least one success then return a 201
      if (m.Success.Any()) return Created(string.Empty, m);

      //If there are errors only then raise a bad request
      if (m.Failure.Any()) return BadRequest(m);

      //If there is nothing then a 200 is fine
      return Ok(m);
    }

    // PATCH api/positions/5
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public ActionResult Patch(int id, [FromBody] JsonPatchDocument<PositionV1PatchModel> patchDoc)
    {
      //TODO: Need more sophisticated patching that only updates what has changed #23
      //TODO: Needs proper validation #24
      var db = _service.GetPosition(id);

      //Preload with existing DB values
      var model = _mapper.ToPatchModel(db);

      if (model == null) throw Lib.Exceptions.NotFound.Position(id);

      //Apply patch doc to model to overwrite what changed only
      patchDoc.ApplyTo(model);

      //Back to entity so it can be updated
      var entity = _mapper.ToEntity(db!.PositionId, db.StockId, model);

      _service.Edit(entity);

      return NoContent();
    }

    // DELETE api/positions/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(int id)
    {
      _service.Delete(id);

      return NoContent();
    }

    // DELETE api/positions/MSFT/symbol
    [HttpDelete("{symbol}/symbol")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult Delete(string symbol)
    {
      
      _service.Delete(UserId, symbol);

      return NoContent();
    }
  }
}
