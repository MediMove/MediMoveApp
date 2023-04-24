using MediMove.Server.Entities;
using MediMove.Server.Services.TransportService;
using MediMove.Shared.Entities;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;

        public TransportController(ITransportService transportService)
        {
            _transportService = transportService;
        }

        // Wyswietlanie transportow dla paramedica i konkretnego dnia
        // Wyswietlanie transportow wszystkich danego dnia
        // Tworzenie nowego transportu
        // Edytowanie istniejącego transportu
        // Usuwanie istniejącego transportu do dogadania bo jesli tego nie bedzie to trzeba dać opcje oznaczenia jako nie wykonany


        [HttpGet("Paramedic/{id}")]
        public async Task<ActionResult<IEnumerable<TransportDTO>>> GetAllForParamedicDay([FromRoute] int id, [FromQuery] DateTime date)
        {
            var result = await _transportService.GetByParamedicId(id, date);

            return Ok(result);
        }

        [HttpGet("Day")]
        public async Task<ActionResult<List<Transport>>> GetAllForDay([FromQuery] DateTime date)
        {
            var result = await _transportService.GetByDay(date);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportDTO>>> GetAll([FromQuery] DateTime date)
        {
            var result = await _transportService.GetAll();

            return Ok(result);
        }


        [HttpPost] 
        public async Task<ActionResult> Create([FromBody] CreateTransportDTO dto )
        {
            await _transportService.Create(dto);// Czy to id jest potrzebne?


            return Ok(); 
        }

        [HttpPatch("{id}")]//Z Route pobrac id transportu do edytowania a z body ciało zedytowanego transportu
        public async Task<ActionResult> Edit([FromRoute] int id, [FromBody] CreateTransportDTO dto)
        {
            await _transportService.Edit(dto, id);

            return Ok();
        }



        //Dodawanie transportu wyswietla kontroller pacjentów. Tam jest wybór istniejącego lub dodanie nowego. Po wybraniu pacjenta przekierowanie na tworzenie transportu z gotowym id.


        
        //post - tworzy
        //put tworzy lub edytuje ( komplet danych potrzebny )
        //patch - edytuje, nie ma potrzeby kompletu danych



    }
}
