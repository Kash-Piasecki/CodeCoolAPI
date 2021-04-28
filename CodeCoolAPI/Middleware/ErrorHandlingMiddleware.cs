using System;
using System.Threading.Tasks;
using CodeCoolAPI.CustomExceptions;
using CodeCoolAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CodeCoolAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequest)
            {
                context.Response.StatusCode = 404;
                _logger.LogWarning(LogMessages.BadRequest);
                await context.Response.WriteAsync(badRequest.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                _logger.LogWarning(LogMessages.EntityNotFound);
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                _logger.LogError(LogMessages.InternalServerError);
                await context.Response.WriteAsync(LogMessages.InternalServerError + e.Message);
            }
        }
    }
}