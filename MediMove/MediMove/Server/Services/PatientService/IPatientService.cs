using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Services.PatientService
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientNameDTO>> GetAll();
        Task<PatientDTO> GetById(int id);
        Task<int> Create(CreatePatientDTO dto);
        Task Edit(int id, CreatePatientDTO dto);
    }
}
