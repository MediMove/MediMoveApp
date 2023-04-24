using MediMove.Server.Entities;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<Patient> GetPatient(int id);
    }
}
