using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers
{
    /// <summary>
    /// Controller that handles global uncaught exceptions.
    /// </summary>
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Action that handles global uncaught
        /// exceptions in development environment.
        /// </summary>
        /// <param name="hostEnvironment">hosting environment</param>
        /// <returns>ProblemDetails response</returns>
        [Route("api/Error/Development")]
        public IActionResult HandleErrorDevelopment(
            IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
                return NotFound();

            var exception = HttpContext.Features
                .Get<IExceptionHandlerFeature>()!.Error;
            
            return Problem(
                title: exception.Message,
                detail: exception.StackTrace);
        }

        /// <summary>
        /// Action that handles global uncaught
        /// exceptions in production environment.
        /// </summary>
        /// <returns>ProblemDetails response</returns>
        [Route("api/Error")]
        public IActionResult HandleError() =>
            Problem();
    }
}
