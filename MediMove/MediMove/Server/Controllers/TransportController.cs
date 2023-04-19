using MediMove.Server.Entities;
using MediMove.Server.Services.TransportService;
using MediMove.Shared.Entities;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("Paramedic/{id}")] 
        public async Task<ActionResult<List<Transport>>> GetAllForParamedicDay([FromRoute] int id, [FromQuery] DateTime date)
        {
            var result = await _transportService.GetByParamedicId(id, date);

            return result;
        }

        [HttpGet("Day")]
        public async Task<ActionResult<List<Transport>>> GetAllForDay([FromQuery] DateTime date)
        {
            var result = await _transportService.GetByDay(date);

            return result;
        }

        [HttpGet]
        public async Task<ActionResult<List<Transport>>> GetAll([FromQuery] DateTime date)
        {
            var result = await _transportService.GetAll();

            return result;
        }

    }
}
