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
            return ToList(_repository.SelectAll());
        }

        // GET api/<TController>/5
        [HttpGet("{id}")]
        public virtual TModel Get(int id)
        {
            return _mapper.ToModel(_repository.Select(id));
        }

        // POST api/<TController>
        [HttpPost]
        public virtual void Post([FromBody] TModel value)
        {
            _repository.Insert(_mapper.ToEntity(value));
        }

        // PUT api/<TController>/5
        [HttpPut("{id}")]
        public virtual void Put(int id, [FromBody] TModel value)
        {
            //Update the ID of the object?
            _repository.Update(_mapper.ToEntity(value));
        }

        // DELETE api/<TController>/5
        [HttpDelete("{id}")]
        public virtual void Delete(int id)
        {
            throw new NotImplementedException("Deletes are not supported by default on purpose.");
        }

        protected IEnumerable<TModel> ToList(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                return Enumerable.Empty<TModel>();

            var lst = entities.Select(x => _mapper.ToModel(x)).ToList();

            return lst;
        }
    }
}
