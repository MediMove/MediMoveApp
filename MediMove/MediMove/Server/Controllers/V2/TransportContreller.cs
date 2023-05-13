using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Shared.Models.DTOs.V2;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.v2
{
    [ApiVersion("2.0")]
    public class TransportController : BaseApiController
    {
        //[HttpGet("{id}")] // jeśli ograniczymy wyświetlane informacje przy podglądzie transportów to tutaj rola paramedic i dispacher i wyswietlać wszystko 
        //public async Task<IActionResult> GetTransport([FromRoute] int id)
        //{
        //    var result = await Mediator.Send(new GetTransportDTO(id));

        //    return result.Match(
        //        result => Ok(result),
        //        errors => Problem(errors));
        //}


        [HttpGet("Paramedic/{id}")] // akcja dla roli dispacher // autoryzacja rolą dispacher
        public async Task<IActionResult> GetTransportsByParamedicAndDay([FromRoute] int id, [FromQuery] int day, [FromQuery] int month, [FromQuery] int year) 
        {
            var result = await Mediator.Send(new GetTransportsByParamedicAndDayQuery(id, new DateTime(year, month, day)));   //(new GetTransportsByParamedicAndDayDTO(id, new DateOnly(year, month, day)));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet("Paramedic")] // akcja do wyświetlania transportów dla paramedica // autoryzacja rolą paramedic
        public async Task<IActionResult> GetTransportsByParamedicAndDay([FromQuery] int day, [FromQuery] int month, [FromQuery] int year)
        {
            int id = 1;//zmienić na id z usera
            var result = await Mediator.Send(new GetTransportsByParamedicAndDayQuery(id, new DateTime(year, month, day)));   //(new GetTransportsByParamedicAndDayDTO(id, new DateOnly(year, month, day)));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        
        [HttpGet("Date")] // Wyświetlanie skróconej listy transportów ( podgląd przy tworzeniu zespołów ) // Autoryzacja rolą dispacher
        public async Task<IActionResult> GetTransportsForDay([FromQuery] int day, [FromQuery] int month, [FromQuery] int year) // zmieniłem z DateTime żeby łatwiej przekazywać przez query
        {
            var result = await Mediator.Send(new GetTransportsByDayQuery(new DateTime(year, month, day)));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }


        [HttpPost] // autoryzacja rolą dispacher
        public async Task<IActionResult> CreateTransport([FromBody] CreateTransportDTO dto)
        {
            var entityId = await Mediator.Send(new CreateTransportCommand(dto));

            return entityId.Match(
                entityId => Ok(),//CreatedAtAction(nameof(GetTransport), new { id = entityId }, null),
                errors => Problem(errors));
        }
        

        [HttpPatch("{id}")]//Autoryzacja rolą dispacher
        public async Task<IActionResult> EditTransport([FromRoute] int id, [FromBody] CreateTransportDTO dto)
        {
            var entity = await Mediator.Send(new UpdateTransportCommand(dto, id));

            return entity.Match(
                entity => NoContent(),
                errors => Problem(errors));
        }

    }
}
