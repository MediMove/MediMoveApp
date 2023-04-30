using MediMove.Server.Exceptions;
using MediMove.Server.Services.TeamService;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// Returns TeamDTO object with given id.
        /// </summary>
        /// <param name="id">
        /// The id of the TeamDTO object to retrieve.
        /// </param>
        /// <response code="200">Returns TeamDTO object with given id</response>
        /// <response code="404">If Team was not found</response>
        [MapToApiVersion("2.0")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetById([FromRoute] int id)
        {
            try
            {
                var team = await _teamService.GetById(id);

                return Ok(team);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
