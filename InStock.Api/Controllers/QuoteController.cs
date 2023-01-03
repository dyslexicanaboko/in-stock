using InStock.Lib.DataAccess;
using InStock.Lib.Entities;
using InStock.Lib.Models;
using InStock.Lib.Services.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    [Route("api/quotes")]
    [ApiController]
    public class QuoteController
        : BaseApiStarterController<IQuote, QuoteEntity, QuoteModel>
    {
        public QuoteController(
            IRepository<QuoteEntity> repository,
            IMapper<IQuote, QuoteEntity, QuoteModel> mapper)
            : base(repository, mapper)
        {

        }
    }
}
