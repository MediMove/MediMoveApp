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

<<<<<<< HEAD
        [HttpGet("{id}")] // Nw czy tak możemy to brać, tutaj wrzucał bym w link id paramedica dla którego pokazane były by transporty
=======
        [HttpGet("Paramedic/{id}")] 
>>>>>>> e8565836ed26a16d64d3f1844040c9702e5ea3de
        public async Task<ActionResult<List<Transport>>> GetAllForParamedicDay([FromRoute] int id, [FromQuery] DateTime date)
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
        public async Task<ActionResult<List<Transport>>> GetAll([FromQuery] DateTime date)
        {
            var result = await _transportService.GetAll();

            return result;
        }

    }
}
