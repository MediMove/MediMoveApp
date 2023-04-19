using MediMove.Server.Data;
using MediMove.Server.Entities;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class TransportRepository : ITransportRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public TransportRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Transport>> GetTransportsForParamedic(int id, DateTime date)
        {
            var transports = await _dbContext.Transports
                .Where(t => t.Team.ParamedicId == id && t.StartTime.Day == date.Day)
                .ToListAsync();

            
            return transports;
        }

        public async Task<List<Transport>> GetTransportsForDay(DateTime date)
        {
            var transports = await _dbContext.Transports
                .Where(t => t.StartTime.Day == date.Day)
                .ToListAsync();

            return transports;
        }
    }
}
