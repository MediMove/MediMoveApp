using Microsoft.AspNetCore.Mvc;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Controllers.v1
{
    public class BillingsController : BaseApiController
    {
        /*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBilling(int id)
        {
            var result = await Mediator.Send(new GetBillingDTO(id));

            return result.Match(
                 result => Ok(result),
                 errors => Problem(errors));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBillings()
        {
            var result = await Mediator.Send(new GetAllBillingsDTO());

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBilling(int id, Billing billing)
        {
            throw new NotImplementedException();
        }
        /*
        [HttpPost]
        public async Task<IActionResult> PostBilling(CreateBillingDTO dto)
        {
            var entity = await Mediator.Send(dto);

            return entity.Match(
                entity => CreatedAtAction(nameof(GetBilling), new { id = entity.Id }, null),
                errors => Problem(errors));
        }

        // DELETE: api/Billings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilling(int id)
        {
            var result = await Mediator.Send(new DeleteBillingDTO(id));

            return result.Match(
                result => NoContent(),
                errors => Problem(errors));
        }
        
        private bool BillingExists(int id)
        {
            return (_context.Billings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        */
    }
}
