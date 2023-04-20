using CommunityToolkit.Diagnostics;
using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/trades")]
    [ApiController]
    public class TradeController
        : BaseApiSecureController
    {
        private readonly ITradeService _service;
        private readonly ITradeMapper _mapper;

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
        public ActionResult<ITrade> Get(int id)
        {
            var entity = _service.GetTrade(id);

            return Ok(_mapper.ToModel(entity));
        }

        // GET api/trade/MSFT/symbol
        [HttpGet("{symbol}/symbol")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ITrade>))]
        public ActionResult<IList<ITrade>> Get(string symbol)
        {
            //TODO: Need to get the UserId from the header or something?
            var lst = _service.GetTrade(UserId, symbol);

            return Ok(_mapper.ToModel(lst));
        }

        // POST api/trade
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ITrade))]
        public async Task<ActionResult<TradeModel>> Post([FromBody] TradeV1CreateModel model)
        {
            //TODO: Need to get the UserId from the header or something?
            var entity = _mapper.ToEntity(UserId, model);

            Guard.IsNotNull(entity);

            await Task.FromResult(_service.Add(entity));

            var m = _mapper.ToModel(entity);

            return CreatedAtAction(nameof(Get), new { id = m!.TradeId }, m);
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
        public ActionResult Delete(string symbol)
        {
            //TODO: Need to get the UserId from the header or something?
            _service.Delete(UserId, symbol);

            return NoContent();
        }
    }
}
