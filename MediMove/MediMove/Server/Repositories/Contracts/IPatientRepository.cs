using MediMove.Server.Models;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient> GetPatient(int id);
        Task<int> Create(Patient dto);
        Task Update(Patient dto);
    }
}
