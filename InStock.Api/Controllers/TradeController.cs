using CommunityToolkit.Diagnostics;
using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
  [Route("api/trades")]
  [ApiController]
  public class TradeController
    : BaseApiSecureController
  {
    private readonly ITradeMapper _mapper;

    private readonly ITradeService _service;

    public TradeController(
      ITradeService service,
      ITradeMapper mapper)
    {
      _service = service;

      _mapper = mapper;
    }

    // GET api/trade/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ITrade))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult<ITrade> Get(int id)
    {
      var entity = _service.GetTrade(id);

      if (entity == null) throw NotFoundExceptions.Trade(id);

      return Ok(_mapper.ToModel(entity));
    }

    // GET api/trade/MSFT/symbol
    [HttpGet("{symbol}/symbol")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ITrade>))]
    public ActionResult<IList<ITrade>> Get(string symbol)
    {
      //TODO: Need to get the UserId from the header or something? 0x202306232308
      var lst = _service.GetTrade(UserId, symbol);

      return Ok(_mapper.ToModel(lst));
    }

    // POST api/trade
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ITrade))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<ActionResult<TradeModel>> Post([FromBody] TradeV1CreateModel model)
    {
      var entity = _mapper.ToEntity(UserId, model);

      Guard.IsNotNull(entity);

      var lst = new List<TradeEntity> { entity };

      var result = (await Task.FromResult(_service.Add(lst))).Single();

      if (!result.IsSuccessful) throw result.Exception!;

      var m = _mapper.ToModel(result.Trade);

      return CreatedAtAction(nameof(Get), new { id = m!.TradeId }, m);
    }

    // POST api/trade/multiple
    [HttpPost("multiple")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TradeV1CreateMultipleModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public async Task<ActionResult<TradeV1CreateMultipleModel>> Post([FromBody] TradeV1CreateModel[] model)
    {
      var entity = _mapper.ToEntity(UserId, model);

      Guard.IsNotNull(entity);
      Guard.IsNotEmpty(entity);

      var lst = entity.ToList();

      //TODO: Need to get the UserId from the header or something? 0x202306232308
      lst.ForEach(x => x.UserId = UserId);

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

    // PATCH api/trades/5
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public ActionResult Patch(int id, [FromBody] JsonPatchDocument<TradeV1PatchModel> patchDoc)
    {
      //TODO: Need more sophisticated patching that only updates what has changed
      var db = _service.GetTrade(id);

      //Preload with existing DB values
      var model = _mapper.ToPatchModel(db);

      if (model == null) throw NotFoundExceptions.Trade(id);

      //Apply patch doc to model to overwrite what changed only
      patchDoc.ApplyTo(model);

      //Back to entity so it can be updated
      var entity = _mapper.ToEntity(db!.TradeId, db.StockId, model);

      _service.Edit(entity);

      return NoContent();
    }

    // DELETE api/trade/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(int id)
    {
      _service.Delete(id);

      return NoContent();
    }

    // DELETE api/trade/MSFT/symbol
    [HttpDelete("{symbol}/symbol")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult Delete(string symbol)
    {
      //TODO: Need to get the UserId from the header or something? 0x202306232308
      _service.Delete(UserId, symbol);

      return NoContent();
    }
  }
}
