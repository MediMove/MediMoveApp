using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Controllers.V1
{
    public class TransportController : BaseApiController
    {
        [HttpGet("{id}")] // jeśli ograniczymy wyświetlane informacje przy podglądzie transportów to tutaj rola paramedic i dispacher i wyswietlać wszystko 
        public async Task<IActionResult> GetTransport([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetTransportQuery(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }


        [HttpGet("Paramedic/{id}")]
        [Authorize(Roles = "Dispatcher")]// akcja dla roli dispacher // autoryzacja rolą dispacher
        public async Task<IActionResult> GetTransportsByParamedicAndDay([FromRoute] int id, [FromQuery] int day, [FromQuery] int month, [FromQuery] int year) 
        {
            var date = new DateTime(year, month, day);
            var result = await Mediator.Send(new GetTransportsByParamedicAndDateRangeQuery(id, date, date));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for getting transports for paramedic by date range.
        /// </summary>
        /// <param name="startDateInclusive">inclusive start date as nullable DateTime</param>
        /// <param name="endDateInclusive">inclusive end date as nullable DateTime</param>
        /// <returns></returns>
        /// <remarks>
        /// Example date: 2023-06-11T12:34:56Z
        /// </remarks>
        [HttpGet("Paramedic")]
        [Authorize(Roles = "Paramedic")]
        public async Task<IActionResult> GetTransportsByParamedicAndDateRange([FromQuery] DateTime? startDateInclusive, [FromQuery] DateTime? endDateInclusive)
        {
            var result = await Mediator.Send(new GetTransportsByParamedicAndDateRangeQuery(getUserId(), startDateInclusive, endDateInclusive));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for getting transports by day and shift while editing plan and adding transport.
        /// </summary>
        /// <param name="year">year as integer</param>
        /// <param name="month">month as integer</param>
        /// <param name="day">day as integer</param>
        /// <param name="shift">shift as ShiftType</param>
        /// <returns>GetTransportsByDayAndShiftResponse</returns>
        [HttpGet("Date")]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetTransportsByDayAndShift([FromQuery] int year, [FromQuery] int month, [FromQuery] int day, [FromQuery] ShiftType shift)
        {
            var result = await Mediator.Send(new GetTransportsByDayAndShiftQuery(new DateTime(year, month, day), shift));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }


        [HttpPost]
        [Authorize(Roles = "Dispatcher")]// autoryzacja rolą dispacher
        public async Task<IActionResult> CreateTransport([FromBody] CreateTransportDTO dto)
        {
            var entity = await Mediator.Send(new CreateTransportCommand(dto));

            return entity.Match(
                entity => CreatedAtAction(nameof(GetTransport), new { id = entity.Id }, null),
                errors => Problem(errors));
        }
        [HttpPost("WithBilling")]
        [Authorize(Roles = "Dispatcher")]// autoryzacja rolą dispacher
        public async Task<IActionResult> CreateTransportWithBilling([FromBody] CreateTransportWithBillingDTO dto)
        {
            var entity = await Mediator.Send(new CreateTransportWithBillingCommand(dto));

            return entity.Match(
                entity => CreatedAtAction(nameof(GetTransport), new { id = entity.Id }, null),
                errors => Problem(errors));
        }


        [HttpPatch("{id}")]
        [Authorize(Roles = "Dispatcher")]//Autoryzacja rolą dispacher
        public async Task<IActionResult> EditTransport([FromRoute] int id, [FromBody] CreateTransportDTO dto)
        {
            var entity = await Mediator.Send(new UpdateTransportCommand(dto, id));

            return entity.Match(
                entity => NoContent(),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for assigning teams to transports.
        /// </summary>
        /// <remarks>
        /// 
        /// {   
        ///     "TransportsToTeams":
        ///     {
        ///         "2":2,
        ///         "30":1
        ///     }
        /// }
        /// </remarks>
        /// <param name="request">AssignTeamsToTransportsRequest</param>
        /// <returns>no content</returns>
        [HttpPatch("AssignTeams")]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> AssignTeamsToTransports([FromBody] AssignTeamsToTransportsRequest request)
        {
            var transports = await Mediator.Send(new AssignTeamsToTransportsCommand(request));

            return transports.Match(
                success => NoContent(),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for deleting transports.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> DeleteTransports([FromBody] DeleteTransportsRequest request)
        {
            var result = await Mediator.Send(new DeleteTransportsCommand(request));

            return result.Match(
                success => NoContent(),
                errors => Problem(errors));
        }
    }
}