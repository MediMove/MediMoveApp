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

        public async Task<List<Transport>> GetTransportsForParamedic(int id, DateTime date)
        {
            var transports = await _dbContext.Transports//.Include( t => t.Patient) -  Ten Include możliwe że będzie potrzebny jak będziemy tworzyć mapper
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
