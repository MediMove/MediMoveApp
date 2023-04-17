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

        public async Task<Patient> GetPatient(int id)
        {
            return await _dbContext.Patients.FindAsync(id);
        }
    }
}
