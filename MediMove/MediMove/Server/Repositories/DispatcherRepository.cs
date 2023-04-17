using MediMove.Server.Data;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
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

        public Task<Dispatcher> GetDispatcher(int id)
        {
            throw new NotImplementedException();
        }
    }
}
