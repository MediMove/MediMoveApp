using MediMove.Server.Data;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public PatientRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _dbContext.Patients.ToListAsync();
        }

        public Task<Patient> GetPatient(int id)
        {
            throw new NotImplementedException();
        }
    }
}
