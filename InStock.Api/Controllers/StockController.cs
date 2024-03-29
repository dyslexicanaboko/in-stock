﻿using InStock.Lib.Entities;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
  [Route("api/stock")]
  [ApiController]
  public class StockController
    : BaseApiSecureController
  {
    private readonly IStockMapper _mapper;

    private readonly IStockService _service;

    public StockController(
      IStockService service,
      IStockMapper mapper)
    {
      _service = service;

      _mapper = mapper;
    }

    // GET api/stock/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IStock))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult<IStock> Get(int id)
    {
      var entity = _service.GetStock(id);

      if (entity == null) throw Lib.Exceptions.NotFound.Stock(id);

      return Ok(_mapper.ToModel(entity));
    }

    // GET api/stock/5
    [HttpGet("{symbol}/symbol")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IStock))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult<IStock> Get(string symbol)
    {
      var entity = _service.GetStock(symbol);

      if (entity == null) throw Lib.Exceptions.NotFound.Stock(symbol);

      return Ok(_mapper.ToModel(entity));
    }

    // PATCH api/stock/5
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult Patch(int id, [FromBody] JsonPatchDocument<StockV1PatchModel> patchDoc)
    {
      var model = new StockV1PatchModel();
      model.StockId = id;

      patchDoc.ApplyTo(model);

      _service.EditNotes(model.StockId, model.Notes);

      return NoContent();
    }

    // POST api/stock
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IStock))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<ActionResult<StockV1CreatedModel>> Post([FromBody] SymbolV1Model model)
    {
      var entity = await _service.Add(model.Symbol);

      var m = _mapper.ToCreatedModel(entity);

      return CreatedAtAction(nameof(Get), new { id = m!.StockId }, m);
    }

    //On the fence about having a DELETE endpoint because it's a serious operation
    //So long as the stock is a real thing, then it can exist indefinitely.
    //There are edge cases such as a company dissolving where the symbol no longer exists.
  }
}
