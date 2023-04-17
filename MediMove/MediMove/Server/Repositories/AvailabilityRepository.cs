using MediMove.Server.Data;
using MediMove.Server.Entities;
using MediMove.Server.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public AvailabilityRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Availability>> GetAvailabilities()
        {
            return await _dbContext.Availabilities.ToListAsync();
        }

        public Task<Availability> GetAvailability(int id)
        {
            throw new NotImplementedException();
        }
    }
}
