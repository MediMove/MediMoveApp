using MediMove.Server.Services.ParamedicService;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.DTOs.temp;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.v1
{
    public class ParamedicController : ApiController
    {
        private readonly IParamedicService _paramedicService;

        public ParamedicController(IParamedicService paramedicService)
        {
            _paramedicService = paramedicService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParamedicDTO>> GetById([FromRoute] int id)
        {
            var paramedic = await _paramedicService.GetById(id);

            return Ok(paramedic);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParamedicDTO>>> GetAll()
        {
            var result = await _paramedicService.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateParamedicDTO dto)
        {
            await _paramedicService.Create(dto);

            return Ok();
        }
    }
}
