using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using MediMove.Server.Application.Teams.Queries.GetTeamQuery;
using MediMove.Server.Application.Teams.Queries.GetTeamsQuery;

namespace MediMove.Server.Controllers.v1
{
    public class TeamController : BaseApiController
    {
        /// <summary>
        /// Returns Team by id as TeamDTO object.
        /// </summary>
        /// <param name="id">
        /// The id of the Team object to retrieve.
        /// </param>
        /// <response code="200">Team by id as TeamDTO object</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetTeamDTO(id));

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
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetTeamsDTO());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /*
        [HttpGet("{startTime:DateTime}")]
        public async Task<ActionResult<IEnumerable<SelectTeamDTO>>> GetAvailable([FromRoute] DateTime startTime, [FromQuery] TransportType tt)
        {
            var result = await _teamService.GetAvailable(startTime, tt);

            return Ok(result);
        }
        */
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
        /// <response code="201">Returns a Team object with the given id</response>
        /// <response code="400">Invaild request</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeamDTO dto)
        {
            var newTeamId = await Mediator.Send(dto);
            
            return newTeamId.Match(
                newTeamId => CreatedAtAction(nameof(GetById), new { id = newTeamId }, null),
                errors => Problem(errors));
        }

        /*
        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] CreateTeamDTO dto)
        {
            var newTeamId = -1;
            try
            {
                newTeamId = await _teamService.Create(dto);
            }
            catch (ForeignKeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            var uri = Url.Link("GetById", new { id = newTeamId });
            return Created(uri, null);
        }
        */
    }
}
