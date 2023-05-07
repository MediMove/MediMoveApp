using MediMove.Server.Services.DispatcherService;
using MediMove.Shared.Models.DTOs.temp;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.V1
{
    public class DispatcherController : ApiController
    {
        private readonly IDispatcherService _dispatcherService;

        public DispatcherController(IDispatcherService dispatcherService)
        {
            _dispatcherService = dispatcherService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DispatcherDTO>> GetById([FromRoute] int id)
        {
            var dispatcher = await _dispatcherService.GetById(id);

            return Ok(dispatcher);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DispatcherDTO>>> GetAll()
        {
            var result = await _dispatcherService.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateDispatcherDTO dto)
        {
            await _dispatcherService.Create(dto);

            return Ok();
        }
    }
}
