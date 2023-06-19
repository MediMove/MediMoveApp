using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Application.Authentication.Queries;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    /// <summary>
    /// Controller for account related endpoints.
    /// </summary>
    public class AccountsController : BaseApiController
    {
        [HttpGet("User/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for registering a new admin.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>created</returns>
        [HttpPost("Register/Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminRequest request)
        {
            var entity = await Mediator.Send(new RegisterAdminCommand(request));

            return entity.Match(
                entity => CreatedAtAction(nameof(GetUserById), new { id = entity.Id }, null),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for registering a new paramedic.
        /// </summary>
        /// <param name="request"><see cref="RegisterParamedicRequest"/></param>
        /// <returns>no content</returns>
        [HttpPost("Register/Paramedic")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterParamedic([FromBody] RegisterParamedicRequest request)
        {
            var entity = await Mediator.Send(new RegisterParamedicCommand(request));

            return entity.Match(
                success => NoContent(),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for registering a new dispatcher.
        /// </summary>
        /// <param name="request"><see cref="RegisterDispatcherRequest"/></param>
        /// <returns>no content</returns>
        [HttpPost("Register/Dispatcher")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterDispatcher([FromBody] RegisterDispatcherRequest request)
        {
            var entity = await Mediator.Send(new RegisterDispatcherCommand(request));

            return entity.Match(
                success => NoContent(),
                errors => Problem(errors));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO dto)
        {
            var result = await Mediator.Send(new LoginQuery(dto));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
    }
}
