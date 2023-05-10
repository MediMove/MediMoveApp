using MediMove.Server.Application.Transports.Queries.GetAllTransportsQuery;
using MediMove.Server.Application.Transports.Queries.GetTransportQuery;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.v1
{
    public class TransportController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransport([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetTransportDTO(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        // Wyswietlanie transportow dla paramedica i konkretnego dnia
        // Wyswietlanie transportow wszystkich danego dnia
        // Tworzenie nowego transportu
        // Edytowanie istniejącego transportu
        // Usuwanie istniejącego transportu do dogadania bo jesli tego nie bedzie to trzeba dać opcje oznaczenia jako nie wykonany


        [HttpGet("Paramedic/{id}")]
        public async Task<IActionResult> GetTransportsByParamedicAndDay([FromBody] GetTransportsByParamedicAndDayDTO dto)
        {
            var result = await Mediator.Send(dto);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
        /*
        [HttpGet("Date")]
        public async Task<IActionResult> GetTransportsForDay([FromQuery] int day, [FromQuery] int month, [FromQuery] int year) // zmieniłem z DateTime żeby łatwiej przekazywać przez query
        {
            var result = await Mediator.Send(dto);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
        */
        [HttpGet]
        public async Task<IActionResult> GetAllTransports()
        {
            var result = await Mediator.Send(new GetAllTransportsDTO());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }


        [HttpPost]
        public async Task<IActionResult> CreateTransport([FromBody] CreateTransportDTO dto)
        {
            var entityId = await Mediator.Send(dto);

            return entityId.Match(
                entityId => CreatedAtAction(nameof(GetTransport), new { id = entityId }, null),
                errors => Problem(errors));
        }
        /*
        [HttpPatch("{id}")]//Z Route pobrac id transportu do edytowania a z body ciało zedytowanego transportu
        public async Task<IActionResult> PatchTransport([FromRoute] int id, [FromBody] CreateTransportDTO dto)
        {
            var entity = await Mediator.Send(dto);

            return entity.Match(
                entity => NoContent(),
                errors => Problem(errors));
        }
        */


        //Dodawanie transportu wyswietla kontroller pacjentów. Tam jest wybór istniejącego lub dodanie nowego. Po wybraniu pacjenta przekierowanie na tworzenie transportu z gotowym id.

        //post - tworzy
        //put tworzy lub edytuje ( komplet danych potrzebny )
        //patch - edytuje, nie ma potrzeby kompletu danych
    }
}
