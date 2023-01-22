using Microsoft.AspNetCore.Mvc;

namespace InStock.Api.Controllers
{
    //TODO: Have to secure this controller
    [ApiController]
    public abstract class BaseApiSecureController
        : ControllerBase
    {
        protected int UserId = 1; //TODO: This needs to be set correctly
    }
}
