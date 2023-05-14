﻿using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Application.Authentication.Queries;
using MediMove.Server.Application.Dispatchers.Queries.GetDispatcherQuery;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    public class AccountsController : BaseApiController
    {
        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            
            var result = await Mediator.Send(new GetUserQuery(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO dto)
        {
            var entityId = await Mediator.Send(new RegisterUserCommand(dto));

            return entityId.Match(
                result => CreatedAtAction(nameof(GetUser), new { id = result }, null),
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