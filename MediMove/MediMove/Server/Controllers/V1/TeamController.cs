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
        /// Returns list of all Teams as TeamDTO objects.
        /// </summary>
        /// <response code="200">Returns list of all Teams as TeamDTO objects</response>
        /// <response code="404">If teams DbSet was null</response>
        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetAllTeamsByDay([FromQuery] int day, [FromQuery] int month, [FromQuery] int year) // Autoryzacja rolą dispacher
        {
            var result = await Mediator.Send(new GetAllTeamsQuery(new DateTime(year, month, day)));//Send(new GetAllTeamsQuery(new DateTime(year, month, day)));

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

        [HttpPatch]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> ChangeWorkingState([FromQuery]int id, [FromQuery]bool state)
        {
            throw new NotImplementedException();

            var result = await Mediator.Send(new ChangeWorkingStateCommand(id, state)); // dodać co zwraca komenda
        }

    }
}
