using MediMove.Server.Data;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class RateRepository : IRateRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public RateRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Rate>> GetRates()
        {
            return await _dbContext.Rates.ToListAsync();
        }

        public async Task<Rate> GetRate(int id)
        {
            return await _dbContext.Rates.FindAsync(id);
        }
    }
}
