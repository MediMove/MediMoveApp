using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class DispatcherRepository : IDispatcherRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public DispatcherRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Dispatcher>> GetDispatchers()
        {
            return await _dbContext.Dispatchers.ToListAsync();
        }

        public async Task<Dispatcher> GetDispatcher(int id)
        {
            return await _dbContext.Dispatchers.FindAsync(id);
        }
    }
}
