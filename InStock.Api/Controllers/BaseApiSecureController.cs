using Microsoft.AspNetCore.Mvc;
using InStock.Lib.DataAccess;
using InStock.Lib.Services.Mappers;
using System.Data;

namespace InStock.Api.Controllers
{
    //TODO: Have to secure this controller
    [ApiController]
    public abstract class BaseApiSecureController
        : ControllerBase
    {

    }
}
