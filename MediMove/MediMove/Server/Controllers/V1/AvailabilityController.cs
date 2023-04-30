using MediMove.Server.Services.AvailabilityService;
using MediMove.Shared.Models.DTOs.temp;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public async Task<ActionResult<AvailabilityDTO>> GetById([FromRoute] int id)
        {
            var availability = await _availabilityService.GetById(id);

            return Ok(availability);
        }

        [HttpGet("Paramedic/{id}")]
        public async Task<ActionResult<IEnumerable<AvailabilityDTO>>> GetByParamedic([FromRoute] int id)
        {
            var result = await _availabilityService.GetByParamedic(id);

            return Ok(result);
        }

        [HttpPost("Paramedic/{id}")]
        public async Task<ActionResult> BulkCreate([FromRoute] int id, [FromBody] IEnumerable<ShiftType> shifts)
        {
            await _availabilityService.BulkCreate(id, shifts);

            return Ok();
        }
    }
}
