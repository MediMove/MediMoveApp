using MediMove.Server.Application.Dispatchers.Queries.GetDispatcherQuery;
using MediMove.Server.Application.Paramedics.Queries.GetAllParamedicsQuery;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.DTOs.temp;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.v1
{
    public class ParamedicController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParamedic([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetDispatcherDTO(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParamedics()
        {
            var result = await Mediator.Send(new GetAllParamedicsDTO());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateParamedic([FromBody] CreateParamedicDTO dto)
        {
            var entityId = await Mediator.Send(dto);

            return entityId.Match(
                entityId => CreatedAtAction(nameof(GetParamedic), new { id = entityId }, null),
                errors => Problem(errors));
        }
    }
}
