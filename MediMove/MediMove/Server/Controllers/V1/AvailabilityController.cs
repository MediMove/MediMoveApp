using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Application.Availabilities.Queries;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    public class AvailabilityController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetAllAvailabilities([FromQuery] int year, [FromQuery] int month, [FromQuery] int day)
        {
            var result = await Mediator.Send(new GetAllAvailabilitiesQuery(new DateTime(year,month,day)));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
       
        [HttpPost]
        [Authorize(Roles = "Paramedic")]
        public async Task<IActionResult> CreateAvailabilities([FromBody] CreateAvailabilitiesDTO availabilities)
        {
            var id = int.Parse(HttpContext.User.Claims
                .FirstOrDefault(c => c.Type.Equals("AccountId")).Value);


            var result = await Mediator.Send(new CreateAvailabilitiesCommand(id, availabilities));

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
    }
}
