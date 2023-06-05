using MediatR;
using MediMove.Server.Application.Patients.Commands;
using MediMove.Server.Application.Patients.Queries;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    public class PatientController : BaseApiController
    {
        [HttpGet] // Wysyła same imiona i nazwiska
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await Mediator.Send(new GetAllPatientsQuery());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetPatient([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetPatientQuery(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDTO dto)
        {
            var entity = await Mediator.Send(new CreatePatientCommand(dto));

            return entity.Match(
                entity => CreatedAtAction(nameof(GetPatient), new { id = entity.Id }, null),
                errors => Problem(errors));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> EditPatient([FromRoute] int id, [FromBody] CreatePatientDTO dto)
        {
            var result = await Mediator.Send(new UpdatePatientCommand(dto, id));

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
    }
}
