using InStock.Lib.Exceptions;
using InStock.Lib.Models.Client;
using Newtonsoft.Json;
using System.Net;

namespace InStock.Api
{
    public class ResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundBaseException ex)
            {
                await Respond(context, HttpStatusCode.NotFound, ex);
            }
            catch (BaseException ex)
            {
                await Respond(context, HttpStatusCode.InternalServerError, ex);
            }

            //TODO: catch Exception - Unhandled exception, which requires logging
        }

        private Task Respond(HttpContext context, HttpStatusCode httpStatusCode, BaseException ex)
        {
            var model = new ErrorModel()
            {
                Code = ex.ErrorCode,
                Message = ex.Message
            };

            var jsonResponse = JsonConvert.SerializeObject(model);

            return Respond(context, httpStatusCode, jsonResponse);
        }

        private async Task Respond(HttpContext context, HttpStatusCode httpStatusCode, string jsonResponse)
        {
            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
