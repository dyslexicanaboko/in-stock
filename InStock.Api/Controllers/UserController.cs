using InStock.Lib.Entities;
using InStock.Lib.Models.Client;
using InStock.Lib.Services;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController
        : BaseApiSecureController
    {
        private readonly IUserService _service;
        private readonly IUserMapper _mapper;

        public UserController(
            IUserService service,
            IUserMapper mapper)
        {
            _service = service;

            _mapper = mapper;
        }

        // GET api/user/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IUser))]
        public ActionResult<IUser> Get(int id)
        {
            var entity = _service.GetUser(id);

            return Ok(_mapper.ToModel(entity));
        }

        // GET api/user/MSFT/symbol
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IUser>))]
        public ActionResult<IList<IUser>> GetAll()
        {
            var lst = _service.GetAllUsers();

            return Ok(_mapper.ToModel(lst));
        }

        // POST api/user
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUser))]
        public async Task<ActionResult<UserV1CreatedModel>> Post([FromBody] UserV1CreateModel model)
        {
            var entity = await Task.FromResult(_service.Add(_mapper.ToEntity(model)));

            var m = _mapper.ToCreatedModel(entity);

            return CreatedAtAction(nameof(Get), new { id = m!.UserId }, m);
        }
    }
}
