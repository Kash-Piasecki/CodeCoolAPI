using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CodeCoolAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware

    {
    public async Task<> InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequestException badRequest)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(badRequest.Message);
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundException.Message);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal Server Error");
        }

    }
    }
}