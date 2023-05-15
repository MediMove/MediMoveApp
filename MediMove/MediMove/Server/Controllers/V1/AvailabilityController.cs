using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Application.Availabilities.Queries;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.v2
{
    public class AvailabilityController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAvailabilities([FromQuery] int year, [FromQuery] int month, [FromQuery] int day)
        {
            var result = await Mediator.Send(new GetAllAvailabilitiesQuery(new DateTime(year,month,day)));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateAvailabilities([FromBody] CreateAvailabilitiesDTO availabilities)
        {
            var result = await Mediator.Send(new CreateAvailabilitiesCommand(availabilities));

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
    }
}
