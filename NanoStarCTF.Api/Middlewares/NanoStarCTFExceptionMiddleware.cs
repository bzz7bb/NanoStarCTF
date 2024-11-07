using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Service.Exceptions;
using Newtonsoft.Json;

namespace NanoStarCTF.Api.Middlewares
{
    public class NanoStarCTFExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<NanoStarCTFExceptionMiddleware> logger;
        public NanoStarCTFExceptionMiddleware(RequestDelegate next, ILogger<NanoStarCTFExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await next.Invoke(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleException(context, ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                //Log
                logger.LogError(ex.ToString());

                await HandleException(context, 500, ex.Message);
            }
        }

        public async Task HandleException(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            await context.Response.WriteAsJsonAsync(new
            {
                Code = code,
                Message = message
            });
        }
    }   
}
