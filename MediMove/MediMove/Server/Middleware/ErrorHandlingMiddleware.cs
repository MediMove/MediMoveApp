using MediMove.Server.Exceptions;

namespace MediMove.Server.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            //catch (ForbidException forbidException)
            //{
            //    context.Response.StatusCode = 403;
            //}
            catch (BadHttpRequestException badHttpRequestException)
            {
                context.Response.StatusCode = badHttpRequestException.StatusCode;
                await context.Response.WriteAsync(badHttpRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong.");
            }
        }
    }
}
