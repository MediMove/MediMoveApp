//using MediMove.Server.Application.Employees.Commands;
using MediMove.Server.Application.Employees.Queries;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    /// <summary>
    /// Controller for employee related endpoints.
    /// </summary>
    public class EmployeeController : BaseApiController
    {
        [HttpGet]
        [Authorize(Roles = "Paramedic")]
        public async Task<IActionResult> GetAllEmployees([FromQuery] bool? isWorking)
        {
            var result = await Mediator.Send(new GetAllEmployeesQuery(isWorking));

            return result.Match(
                success => Ok(success),
                errors => Problem(errors));
        }
    }
}
