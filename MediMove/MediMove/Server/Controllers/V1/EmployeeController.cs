//using MediMove.Server.Application.Employees.Commands;
using MediMove.Server.Application.Employees.Commands;
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
        /// <summary>
        /// Action for getting all employees.
        /// </summary>
        /// <param name="isWorking">specifies whether to filter employees by their working status</param>
        /// <returns><see cref="GetAllEmployeesResponse"/></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllEmployees([FromQuery] bool? isWorking)
        {
            var result = await Mediator.Send(new GetAllEmployeesQuery(isWorking));

            return result.Match(
                success => Ok(success),
                errors => Problem(errors));
        }

        [HttpPost("Report/Multiple")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeesInMonthByHoursAndSalary(
            [FromBody] GetEmployeesInMonthByHoursAndSalaryRequest request)
        {
            var result = await Mediator.Send(new GetEmployeesInMonthByHoursAndSalaryQuery(request.StartTime, request.EndTime, request.StartPaymentsHours, request.EndPaymentsHours, request.StartPaymentsSum, request.EndPaymentsSum));

            return result.Match(
                success => Ok(success),
                errors => Problem(errors));
        }

        [HttpGet("Report/Single")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeeRatesByIdAndSalary(
            [FromQuery] int id,
            [FromQuery] DateTime startTime,
            [FromQuery] DateTime endTime)
        {
            var result = await Mediator.Send(new GetEmployeeRatesByIdAndDatesQuery(id, startTime, endTime));

            return result.Match(
                success => Ok(success),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for updating employees.
        /// </summary>
        /// <param name="request"><see cref="PutEmployeesRequest"/></param>
        /// <returns>no content</returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutEmployees([FromBody] PutEmployeesRequest request)
        {
            var result = await Mediator.Send(new PutEmployeesCommand(request));

            return result.Match(
                success => NoContent(),
                errors => Problem(errors));
        }
    }
}
