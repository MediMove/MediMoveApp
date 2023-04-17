using MediMove.Server.Data;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
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

        public Task<PersonalInformation> GetPersonalInformation(int id)
        {
            throw new NotImplementedException();
        }
    }
}
