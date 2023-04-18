using MediMove.Server.Services.TransportService;
using MediMove.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;

        public TransportController(ITransportService transportService)
        {
            _transportService = transportService;
        }

        [HttpGet("id")] // Nw czy tak możemy to brać, tutaj wrzucał bym w link id paramedica dla którego pokazane były by transporty
        public async Task<ActionResult<List<Transport>>> GetAllForParamedicDay([FromRoute] int id, [FromQuery] DateTime date)
        {
            var result = await _transportService.GetByParamedicId(id, date);

            return result;
        }

        [HttpGet]
        public async Task<ActionResult<List<Transport>>> GetAllForDay([FromBody] DateTime date)
        {
            var result = await _transportService.GetByDay(date);

            return result;
        }

    }
}
