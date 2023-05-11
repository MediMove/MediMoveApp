using MediMove.Server.Application.Dispatchers.Queries.GetAllDispatchersQuery;
using MediMove.Server.Application.Dispatchers.Queries.GetDispatcherQuery;
using MediMove.Shared.Models.DTOs.temp;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Controllers.v1
{
    public class DispatcherController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDispatcher([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetDispatcherDTO(id));

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDispatchers()
        {
            var result = await Mediator.Send(new GetAllDispatchersDTO());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDispatcher([FromBody] CreateDispatcherDTO dto)
        {
            var entityId = await Mediator.Send(dto);

            return entityId.Match(
                entityId => CreatedAtAction(nameof(GetDispatcher), new { id = entityId }, null),
                errors => Problem(errors));
        }
    }
}
