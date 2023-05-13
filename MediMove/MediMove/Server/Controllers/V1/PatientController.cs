//using MediMove.Server.Models;
//using MediMove.Shared.Models.DTOs.V2;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace MediMove.Server.Controllers.v1
//{
//    public class PatientController : BaseApiController
//    {
//        //1 - Lista imie i nazwisko
//        //2 - details o jednym
//        //3 - dodawanie pacjenta
//        //4 - edytowanie pacjenta



//        [HttpGet] // Wywyła same imiona i nazwiska
//        public async Task<IActionResult> GetAllPatients()
//        {
//            var result = await Mediator.Send(new GetAllPatientsDTO());

//            return result.Match(
//                result => Ok(result),
//                errors => Problem(errors));
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetPatient([FromRoute] int id)
//        {
//            var result = await Mediator.Send(new GetPatientDTO(id));

//            return result.Match(
//                result => Ok(result),
//                errors => Problem(errors));
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDTO dto)
//        {
//            var entityId = await Mediator.Send(dto);

//            return entityId.Match(
//                entityId => CreatedAtAction(nameof(GetPatient), new { id = entityId }, null),
//                errors => Problem(errors));
//        }
//        /*
//        [HttpPatch("{id}")]
//        public async Task<IActionResult> PatchPatient([FromRoute] int id, [FromBody] CreatePatientDTO dto)
//        {
//            throw new NotImplementedException();

//            var result = await Mediator.Send(dto);

//            return result.Match(
//                result => NoContent(),
//                errors => Problem(errors));
//        }
//        */
//    }
//}
