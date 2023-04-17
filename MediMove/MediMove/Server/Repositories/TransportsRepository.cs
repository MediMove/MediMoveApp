using MediMove.Server.Data;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class TransportsRepository : ITransportRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public TransportsRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transport>> GetTransports()
        {
            return await _dbContext.Transports.ToListAsync();
        }

        public Task<Transport> GetTransport(int id)
        {
            throw new NotImplementedException();
        }
    }
}
