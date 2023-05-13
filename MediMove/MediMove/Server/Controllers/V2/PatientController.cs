using MediatR;
using MediMove.Server.Application.Patients.Commands;
using MediMove.Server.Application.Patients.Queries;
using MediMove.Shared.Models.DTOs.V2;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V2
{
    [ApiVersion("2.0")]
    public class PatientController : BaseApiController
    {
        [HttpGet] // Wysyła same imiona i nazwiska
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await Mediator.Send(new GetAllPatientsQuery());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetPatientQuery(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDTO dto)
        {
            var entityId = await Mediator.Send(new CreatePatientCommand(dto));

            return entityId.Match(
                entityId => CreatedAtAction(nameof(GetPatient), new { id = entityId }, null),
                errors => Problem(errors));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditPatient([FromRoute] int id, [FromBody] CreatePatientDTO dto)
        {
            var result = await Mediator.Send(new UpdatePatientCommand(dto, id));

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
    }
}
