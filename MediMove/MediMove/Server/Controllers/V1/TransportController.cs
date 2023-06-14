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
            var result = await Mediator.Send(new GetTransportsByParamedicAndDateRangeQuery(GetUserId(), startDateInclusive, endDateInclusive));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        /// <summary>
        /// Action for getting transports by day and shift while editing plan and adding transport.
        /// </summary>
        /// <param name="date">date as DateTime</param>
        /// <param name="shift">shift as ShiftType</param>
        /// <returns>>GetTransportsByDayAndShiftResponse</returns>
        /// <remarks>
        /// Example date: 2023-06-11T12:34:56Z
        /// </remarks>
        [HttpGet("Date")]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> GetTransportsByDayAndShift([FromQuery] DateTime date, [FromQuery] ShiftType shift)
        {
            var result = await Mediator.Send(new GetTransportsByDayAndShiftQuery(date, shift));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }



        [HttpPost]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> CreateTransport([FromBody] CreateTransportDTO dto)
        {
            var entity = await Mediator.Send(new CreateTransportCommand(dto));

            return entity.Match(
                entity => CreatedAtAction(nameof(GetTransport), new { id = entity.Id }, null),
                errors => Problem(errors));
        }

        [HttpPost("WithBilling")]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> CreateTransportWithBilling([FromBody] CreateTransportWithBillingDTO dto)
        {
            var entity = await Mediator.Send(new CreateTransportWithBillingCommand(dto));

            return entity.Match(
                entity => CreatedAtAction(nameof(GetTransport), new { id = entity.Id }, null),
                errors => Problem(errors));
        }


        [HttpPatch("{id}")]
        [Authorize(Roles = "Dispatcher")]
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
        /// Action for cancelling transports.
        /// </summary>
        /// <param name="request">CancelTransportsRequest</param>
        /// <returns>no content</returns>
        [HttpPatch]
        [Authorize(Roles = "Dispatcher")]
        public async Task<IActionResult> CancelTransports([FromBody] CancelTransportsRequest request)
        {
            var result = await Mediator.Send(new CancelTransportsCommand(request));

            return result.Match(
                success => NoContent(),
                errors => Problem(errors));
        }
    }
}