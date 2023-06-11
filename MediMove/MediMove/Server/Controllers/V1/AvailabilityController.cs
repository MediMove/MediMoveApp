using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Application.Availabilities.Queries;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    /// <summary>
    /// Controller for availability related endpoints.
    /// </summary>
    public class AvailabilityController : BaseApiController
    {
        /// <summary>
        /// Action for getting available paramedics by day and shift.
        /// </summary>
        /// <param name="year">year as integer</param>
        /// <param name="month">month as integer</param>
        /// <param name="day">day as integer</param>
        /// <param name="shift">shift as ShiftType</param>
        /// <returns>GetAvailableParamedicsByDayAndShiftResponse</returns>
        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetAvailableParamedicsByDayAndShift([FromQuery] int year, [FromQuery] int month, [FromQuery] int day, [FromQuery] ShiftType shift)
        { 
            var result = await Mediator.Send(new GetAvailableParamedicsByDayAndShiftQuery(new DateTime(year, month, day), shift));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for getting availabilities for paramedic by date range.
        /// </summary>
        /// <param name="startDateInclusive">inclusive start date as nullable DateTime</param>
        /// <param name="endDateInclusive">inclusive end date as nullable DateTime</param>
        /// <returns>GetAvailabilitiesForParamedicByDateRangeResponse</returns>
        /// <remarks>
        /// Example date: 2023-06-11T12:34:56Z
        /// </remarks>
        [HttpGet("Paramedic")]
        [Authorize(Roles = "Paramedic")]
        public async Task<IActionResult> GetAvailabilitiesForParamedicByDateRange([FromQuery] DateTime? startDateInclusive, [FromQuery] DateTime? endDateInclusive)
        {
            var result = await Mediator.Send(new GetAvailabilitiesForParamedicByDateRangeQuery(getUserId(), startDateInclusive, endDateInclusive));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for creating availabilities.
        /// </summary>
        /// <param name="request">CreateAvailabilitiesRequest</param>
        /// <returns>no content</returns>
        [HttpPost]
        [Authorize(Roles = "Paramedic")]
        public async Task<IActionResult> CreateAvailabilities([FromBody] CreateAvailabilitiesRequest request)
        {
            var result = await Mediator.Send(new CreateAvailabilitiesCommand(getUserId(), request));

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for deleting availabilities.
        /// </summary>
        /// <param name="request">DeleteAvailabilitiesRequest</param>
        /// <returns>no content</returns>
        [HttpDelete]
        [Authorize(Roles = "Paramedic")]
        public async Task<IActionResult> DeleteAvailabilities([FromBody] DeleteAvailabilitiesRequest request)
        {
            var result = await Mediator.Send(new DeleteAvailabilitiesCommand(getUserId(), request));

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
    }
}