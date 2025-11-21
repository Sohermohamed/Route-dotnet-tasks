using ECommerce.Domain.Exceptions;
using ECommerce.Shared.ErrorModels;

namespace ECommerce.Middleware
{
    public class CutomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CutomExceptionMiddleware> logger;

        public CutomExceptionMiddleware(RequestDelegate Next , ILogger<CutomExceptionMiddleware> logger)
        {
            next = Next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);

                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var response = new ErrorToReturn()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"End Point {context.Request.Path} Not Found",
                    };

                    await context.Response.WriteAsJsonAsync(response);
                }

            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                var response = new ErrorToReturn()
                {
                    Message = e.Message,
                };

                context.Response.StatusCode = e switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnAutherizedException => StatusCodes.Status401Unauthorized,
                    BadRequestException badRequestException => GetBadRequestErrors(badRequestException,response),
                    _ => StatusCodes.Status500InternalServerError
                };
                response.StatusCode = context.Response.StatusCode;

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(response);

            }
        }

        private int GetBadRequestErrors(BadRequestException exception,ErrorToReturn response)
        {
            response.Errors = exception.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
