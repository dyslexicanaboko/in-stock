using InStock.Lib.Entities;
using InStock.Lib.Exceptions;
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
    private readonly IUserMapper _mapper;

    private readonly IUserService _service;

    public UserController(
      IUserService service,
      IUserMapper mapper)
    {
      _service = service;

      _mapper = mapper;
    }

    // GET api/users/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IUser))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
    public ActionResult<IUser> Get(int id)
    {
      var entity = _service.GetUser(id);

      if (entity == null) throw Lib.Exceptions.NotFound.User(id);

      return Ok(_mapper.ToModel(entity));
    }

    // GET api/users
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<IUser>))]
    public ActionResult<IList<IUser>> GetAll()
    {
      var lst = _service.GetAllUsers();

      return Ok(_mapper.ToModel(lst));
    }

    // POST api/users
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IUser))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
    public async Task<ActionResult<UserV1CreatedModel>> Post([FromBody] UserV1CreateModel model)
    {
      var entity = await Task.FromResult(_service.Add(_mapper.ToEntity(model)));

      var m = _mapper.ToCreatedModel(entity);

      return CreatedAtAction(nameof(Get), new { id = m!.UserId }, m);
    }

    //No PATCH or DELETE endpoints for now on purpose
  }
}
