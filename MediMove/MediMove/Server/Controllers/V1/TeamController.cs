using Microsoft.AspNetCore.Mvc;
using MediMove.Server.Application.Teams.Queries;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Controllers.V1
{
    /// <summary>
    /// Controller for managing teams.
    /// </summary>
    public class TeamController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetTeamQuery(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for getting teams by day and shift.
        /// </summary>
        /// <param name="year">year as integer</param>
        /// <param name="month">month as integer</param>
        /// <param name="day">day as integer</param>
        /// <param name="shift">shift as ShiftType</param>
        /// <returns>GetTeamsByDayAndShiftResponse</returns>
        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetTeamsByDayAndShift([FromQuery] int year, [FromQuery] int month, [FromQuery] int day, [FromQuery] ShiftType shift)
        {
            var result = await Mediator.Send(new GetTeamsByDayAndShiftQuery(new DateTime(year, month, day), shift));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for creating teams.
        /// </summary>
        /// <param name="request">CreateTeamsRequest</param>
        /// <returns>no content</returns>
        [HttpPost]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> CreateTeams([FromBody] CreateTeamsRequest request)
        {
            var result = await Mediator.Send(new CreateTeamsCommand(request));

            return result.Match(
                success => NoContent(),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for deleting teams.
        /// </summary>
        /// <param name="request">DeleteTeamsRequest</param>
        /// <returns>no content</returns>
        [HttpDelete]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> DeleteTeams([FromBody] DeleteTeamsRequest request)
        {
            var result = await Mediator.Send(new DeleteTeamsCommand(request));

            return result.Match(
                success => NoContent(),
                errors => Problem(errors));
        }
    }
}
