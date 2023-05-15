using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Application.Authentication.Queries;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
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

        [HttpPost("Register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO dto)
        {
            var entityId = await Mediator.Send(new RegisterUserCommand(dto));

            return entityId.Match(
                result => CreatedAtAction(nameof(GetUserById), new { id = result }, null),
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
