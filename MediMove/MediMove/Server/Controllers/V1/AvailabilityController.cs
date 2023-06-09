﻿using MediMove.Server.Application.Availabilities.Commands;
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
        /// <param name="date">date as DateTime</param>
        /// <param name="shift">shift as ShiftType</param>
        /// <returns>GetAvailableParamedicsByDateAndShiftResponse</returns>
        /// <remarks>
        /// Example date: 2023-06-11
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetAvailableParamedicsByDateAndShift([FromQuery] DateTime date, [FromQuery] ShiftType shift)
        { 
            var result = await Mediator.Send(new GetAvailableParamedicsByDateAndShiftQuery(date, shift));

            return result.Match(
                success => Ok(success),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for getting availabilities for paramedic by date range.
        /// </summary>
        /// <param name="startDateInclusive">inclusive start date as nullable DateTime</param>
        /// <param name="endDateInclusive">inclusive end date as nullable DateTime</param>
        /// <returns>GetAvailabilitiesForParamedicByDateRangeResponse</returns>
        /// <remarks>
        /// Example date: 2023-06-11
        /// </remarks>
        [HttpGet("Paramedic")]
        [Authorize(Roles = "Paramedic")]
        public async Task<IActionResult> GetAvailabilitiesForParamedicByDateRange([FromQuery] DateTime? startDateInclusive, [FromQuery] DateTime? endDateInclusive)
        {
            var result = await Mediator.Send(new GetAvailabilitiesByParamedicAndDateRangeQuery(GetUserId(), startDateInclusive, endDateInclusive));

            return result.Match(
                success => Ok(success),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for creating availabilities.
        /// </summary>
        /// <param name="request">CreateAvailabilitiesRequest</param>
        /// <returns>no content</returns>
        /// <remarks>
        /// {
        ///     "Availabilities": {
        ///         "2023-06-14": null,
        ///         "2023-06-15": 0,
        ///         "2023-06-16": 1
        ///     }
        /// }
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Paramedic")]
        public async Task<IActionResult> CreateAvailabilities([FromBody] CreateAvailabilitiesRequest request)
        {
            var result = await Mediator.Send(new CreateAvailabilitiesCommand(GetUserId(), request));

            return result.Match(
                success => NoContent(),
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
            var result = await Mediator.Send(new DeleteAvailabilitiesCommand(GetUserId(), request));

            return result.Match(
                success => NoContent(),
                errors => Problem(errors));
        }
    }
}