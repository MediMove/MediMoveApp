using MediMove.Server.Exceptions;
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
        
        [HttpGet("{id}", Name = "GetById")]
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
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAll()
        {
            var teams = await _teamService.GetAll();
            return Ok(teams);
        }
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectTeamDTO>>> GetFreeForStartDate([FromBody] DateTime dt)
        {
            var result = await _teamService.GetFreeForStartDate(dt);

            return Ok(result);
        }
        */
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateTeamDTO dto)
        {
            var newTeamId = -1;
            try
            {
                newTeamId = _teamService.Create(dto);
            }
            catch (InvalidDateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ForeignKeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            
            var uri = Url.Link("GetById", new { id = newTeamId });
            return Created(uri, null);
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
