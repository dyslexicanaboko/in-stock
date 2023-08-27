using InStock.Lib.Entities;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
  [Route("api/quotes")]
  [ApiController]
  public class QuoteController
    : BaseApiSecureController
  {
    private readonly IQuoteMapper _mapper;

    private readonly IQuoteService _service;

    public QuoteController(
      IQuoteService service,
      IQuoteMapper mapper)
    {
      _service = service;

      _mapper = mapper;
    }

    // GET api/quote/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQuote))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult<IQuote> Get(int id)
    {
      var entity = _service.GetQuote(id);

      if (entity == null) throw Lib.Exceptions.NotFound.Quote(id);

      return Ok(_mapper.ToModel(entity));
    }

    // GET api/quote/MSFT
    [HttpGet("{symbol}/symbol")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQuote))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult<IQuote> Get(string symbol)
    {
      var entity = _service.GetQuote(symbol);

      if (entity == null) throw Lib.Exceptions.NotFound.Quote(symbol);

      return Ok(_mapper.ToModel(entity));
    }

    // POST api/quote
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IQuote))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<ActionResult<QuoteV1CreatedModel>> Post([FromBody] SymbolV1Model model)
    {
      var entity = await _service.Add(model.Symbol);

      var m = _mapper.ToCreatedModel(entity);

      return CreatedAtAction(nameof(Get), new { id = m.QuoteId }, m);
    }

    //PATCH and DELETE is not supported for Quotes.
    //Quotes can only be created and retrieved.
  }
}
