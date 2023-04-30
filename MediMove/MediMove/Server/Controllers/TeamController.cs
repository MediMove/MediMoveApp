using MediMove.Server.Exceptions;
using MediMove.Server.Models;
using MediMove.Server.Services.TeamService;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers
{
    public class TeamController : V1ApiController
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

        /// <summary>
        /// Returns all Teams as TeamDTO objects.
        /// </summary>
        /// <response code="200">Returns all Teams as TeamDTO objects</response>
        /// <response code="404">If Team DbSet was not found</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAll()
        {
            try
            {
                var teams = await _teamService.GetAll();
                return Ok(teams);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectTeamDTO>>> GetFreeForStartDate([FromBody] DateTime dt)
        {
            var result = await _teamService.GetFreeForStartDate(dt);

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
        public async Task<ActionResult> Create([FromBody] CreateTeamDTO dto)
        {
            Team newTeam;
            try
            {
                newTeam = _teamService.Create(dto);
            }
            catch (InvalidDateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ForeignKeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = newTeam.Id }, null);
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
