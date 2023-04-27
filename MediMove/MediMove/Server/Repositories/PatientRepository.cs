using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
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
            return await _dbContext.Patients.Include(p => p.PersonalInformation).ToListAsync();
        }

        public async Task<Patient> GetPatient(int id)
        {
            return await _dbContext.Patients.Include(p => p.PersonalInformation).FirstAsync(p=> p.Id == id);
        }

        public async Task<int> Create(Patient dto)
        {
            throw new NotImplementedException(); // trzeba zwrócić id 
        }

        public async Task Update(Patient dto)
        {
            throw new NotImplementedException();

        }
    }
}
