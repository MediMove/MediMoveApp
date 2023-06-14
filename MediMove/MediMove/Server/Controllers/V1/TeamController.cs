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
                success => Ok(success),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for getting teams by day and shift.
        /// </summary>
        /// <param name="date">date as DateTime</param>
        /// <param name="shift">shift as ShiftType</param>
        /// <returns>GetTeamsByDayAndShiftResponse</returns>
        /// <remarks>
        /// Example date: 2023-06-11T12:34:56Z
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetTeamsByDayAndShift([FromQuery] DateTime date, [FromQuery] ShiftType shift)
        {
            var result = await Mediator.Send(new GetTeamsByDayAndShiftQuery(date, shift));

            return result.Match(
                success => Ok(success),
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
