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
        private readonly IQuoteService _service;
        private readonly IQuoteMapper _mapper;

        public QuoteController(
            IQuoteService service,
            IQuoteMapper mapper)
        {
            _service = service;

            _mapper = mapper;
        }

        // GET api/quote/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQuote))]
        public ActionResult<IQuote> Get(int id)
        {
            var entity = _service.GetQuote(id);

            return Ok(_mapper.ToModel(entity));
        }

        // GET api/quote/5
        [HttpGet("{symbol}/symbol")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQuote))]
        public ActionResult<IQuote> Get(string symbol)
        {
            var entity = _service.GetQuote(symbol);

            //TODO: The stock not found message sucks, need to make it more clear
            if (entity == null) return NotFound();

            return Ok(_mapper.ToModel(entity));
        }

        // POST api/quote
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IQuote))]
        public async Task<ActionResult<QuoteV1CreatedModel>> Post([FromBody] SymbolV1Model model)
        {
            var entity = await _service.Add(model.Symbol);

            var m = _mapper.ToCreatedModel(entity);

            return CreatedAtAction(nameof(Get), new { id = m.QuoteId }, m);
        }
    }
}
