using MediMove.Server.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers
{
    [Route("api/[controller]")]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            string title;
            int statusCode;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    title = notFoundException.Message;
                    statusCode = notFoundException.StatusCode;
                    break;
                case BadHttpRequestException badRequestException:
                    title = badRequestException.Message;
                    statusCode = badRequestException.StatusCode;
                    break;
                default:
                    return Problem();
            }
            return Problem(title: title, statusCode: statusCode);
        }
    }
}
