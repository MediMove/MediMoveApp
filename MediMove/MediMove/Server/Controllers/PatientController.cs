using MediMove.Server.Entities;
using MediMove.Server.Services.PatientService;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediMove.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        //1 - Lista imie i nazwisko
        //2 - details o jednym
        //3 - dodawanie pacjenta
        //4 - edytowanie pacjenta



        [HttpGet] // Wywyła same imiona i nazwiska
        public async Task<ActionResult<IEnumerable<PatientNameDTO>>> GetAll()
        {
            var patients = await _patientService.GetAll();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> Get(int id)
        {
            var patient = await _patientService.Get(id);

            return Ok(patient);

        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreatePatientDTO dto)
        {
            var newPatientId = await _patientService.Create(dto);

            return Ok(newPatientId);
        }

        
        [HttpPatch("{id}")]
        public async Task<ActionResult> Edit([FromRoute]int id, [FromBody] CreatePatientDTO dto)
        {
           

            return Ok();
        }

       
    }
}
