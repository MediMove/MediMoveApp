using MediMove.Server.Application.Availabilities.Queries.GetAllAvailabilitiesQuery;
using MediMove.Server.Application.Availabilities.Queries.GetAvailabilityQuery;
using MediMove.Shared.Models.DTOs.temp;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.v1
{
    public class AvailabilityController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAvailability([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetAvailabilityDTO(id));

            return result.Match(
                 result => Ok(result),
                 errors => Problem(errors));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAvailabilities()
        {
            var result = await Mediator.Send(new GetAllAvailabilitiesDTO());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
        /*
        [HttpGet("Paramedic/{id}")]
        public async Task<ActionResult> GetAvailabilitiesByParamedic([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetAvailabilitiesGetByParamedicDTO(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
        */
        [HttpPost]
        public async Task<IActionResult> CreateAvailabilities([FromBody] CreateAvailabilitiesDTO availabilities)
        {
            var result = await Mediator.Send(availabilities);

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
    }
}
