using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Taskify.Utils.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<ExceptionHandlingMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                if (httpContext.Response.StatusCode == StatusCodes.Status200OK)
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
