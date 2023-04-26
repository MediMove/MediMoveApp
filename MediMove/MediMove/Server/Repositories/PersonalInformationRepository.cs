using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class PersonalInformationRepository : IPersonalInformationRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public PersonalInformationRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PersonalInformation>> GetPersonalInformations()
        {
            return await _dbContext.PersonalInformations.ToListAsync();
        }

        public async Task<PersonalInformation> GetPersonalInformation(int id)
        {
            return await _dbContext.PersonalInformations.FindAsync(id);
        }
    }
}
