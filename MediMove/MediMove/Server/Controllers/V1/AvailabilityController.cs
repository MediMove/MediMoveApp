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
        public async Task<IActionResult> GetAvailableParamedicsByDayAndShift([FromQuery] int year, [FromQuery] int month, [FromQuery] int day, [FromQuery] ShiftType shift)
        { 
            var result = await Mediator.Send(new GetAvailableParamedicsByDayAndShiftQuery(new DateTime(year, month, day), shift));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /// <summary>
        /// Creates availabilities for a given paramedic.
        /// </summary>
        /// <param name="dto">CreateAvailabilitiesDTO object</param>
        [HttpPost]
        [Authorize(Roles = "Paramedic")]
        public async Task<IActionResult> CreateAvailabilities([FromBody] CreateAvailabilitiesRequest dto)
        {
            var result = await Mediator.Send(new CreateAvailabilitiesCommand(getUserId(), dto));

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
    }
}
