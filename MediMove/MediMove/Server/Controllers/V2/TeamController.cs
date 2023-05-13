using Microsoft.AspNetCore.Mvc;
using MediMove.Server.Application.Teams.Queries;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Shared.Models.DTOs.V2;

namespace MediMove.Server.Controllers.v2
{
    [ApiVersion("2.0")]
    public class TeamController : BaseApiController
    {

        /// <summary>
        /// Returns list of all Teams as TeamDTO objects.
        /// </summary>
        /// <response code="200">Returns list of all Teams as TeamDTO objects</response>
        /// <response code="404">If teams DbSet was null</response>
        [HttpGet]
        public async Task<IActionResult> GetAllTeamsByDay([FromQuery] int day, [FromQuery] int month, [FromQuery] int year) // Autoryzacja rolą dispacher
        {
            var result = await Mediator.Send(new GetAllTeamsQuery(new DateTime(year, month, day)));//Send(new GetAllTeamsQuery(new DateTime(year, month, day)));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }


        /// <summary>
        /// Creates a specific Team.
        /// </summary>
        /// <param name="dto">CreateTeamDTO object</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST
        ///     {
        ///          "driverId": 2,
        ///          "paramedicId": 3,
        ///          "day": "2023-07-01"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">Invaild request</response>
        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDTO dto) // Przerobić na tworzenie listy teamów na dany dzień
        {
            var entityId = await Mediator.Send(new CreateTeamCommand(dto));

            return entityId.Match(
                entityId => Ok(),  //entityId => CreatedAtAction(nameof(GetTeam), new { id = entityId }, null),
                errors => Problem(errors));
        }


    }
}
