using MediMove.Server.Data;
using MediMove.Server.Models;
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

        public async Task<Availability> GetAvailability(int id)
        {
            return await _dbContext.Availabilities.FindAsync(id);
        }

        public async Task<IEnumerable<Availability>> GetByParamedic(int id)
        {
            var availabilities = await _dbContext.Availabilities
                .Where(t =>
                    t.ParamedicId == id)
                .ToListAsync();

            return availabilities;
        }
    }
}
