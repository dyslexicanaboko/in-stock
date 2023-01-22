using Microsoft.AspNetCore.Mvc;
using InStock.Lib.DataAccess;
using InStock.Lib.Services.Mappers;
using System.Data;

namespace InStock.Api.Controllers
{
    /// <summary>
    /// Rigid Base API Controller to get a concept off the ground. Only good if the target model is to operate with
    /// basic crud. This controller will not lend itself to different types of models being used or complex business
    /// logic.
    /// </summary>
    /// <typeparam name="TSharedInterface">Shared interface between Entity and Model</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TModel">Model type</typeparam>
    [ApiController]
    public abstract class BaseApiStarterController<TSharedInterface, TEntity, TModel> 
        : ControllerBase
        where TSharedInterface : class
        where TEntity : class, new()
        where TModel : class, new()
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper<TSharedInterface, TEntity, TModel> _mapper;

        public BaseApiStarterController(
            IRepository<TEntity> repository,
            IMapper<TSharedInterface, TEntity, TModel> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        // GET: api/<TController>
        [HttpGet]
        public virtual IEnumerable<TModel> Get()
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return ToList(_repository.SelectAll());
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        // GET api/<TController>/5
        [HttpGet("{id}")]
        public virtual TModel? Get(int id)
        {
            var model = _repository.Select(id);

            if (model == null) return null;

            return _mapper.ToModel(model);
        }

        // POST api/<TController>
        [HttpPost]
        public virtual void Post([FromBody] TModel value)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            _repository.Insert(_mapper.ToEntity(value));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        // PUT api/<TController>/5
        [HttpPut("{id}")]
        public virtual void Put(int id, [FromBody] TModel value)
        {
            //Update the ID of the object?
#pragma warning disable CS8604 // Possible null reference argument.
            _repository.Update(_mapper.ToEntity(value));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        // DELETE api/<TController>/5
        [HttpDelete("{id}")]
        public virtual void Delete(int id)
        {
            throw new NotImplementedException("Deletes are not supported by default on purpose.");
        }

        protected IEnumerable<TModel?> ToList(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return Enumerable.Empty<TModel>();

            var lst = entities.Select(x => _mapper.ToModel(x)).ToList();

            return lst;
        }
    }
}
