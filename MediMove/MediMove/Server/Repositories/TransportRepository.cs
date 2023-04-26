using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;
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

        public async Task<IEnumerable<Transport>> GetTransportsForParamedic(int id, DateOnly date)
        {
            var transports = await _dbContext.Transports
                .Where(t => 
                    (t.Team.ParamedicId == id || t.Team.DriverId == id) &&
                    t.StartTime.Day == date.Day &&
                    t.StartTime.Month == date.Month &&              // bez sensu było szukanie po samym dniu
                    t.StartTime.Year == date.Year
                    )
                .ToListAsync();

            
            return transports;
        }

        public async Task<IEnumerable<Transport>> GetTransportsForDay(DateOnly date)
        {
            var transports = await _dbContext.Transports
                .Where(t => 
                    t.StartTime.Day == date.Day &&
                    t.StartTime.Month == date.Month &&              // bez sensu było szukanie po samym dniu
                    t.StartTime.Year == date.Year
                ).ToListAsync();

            return transports;
        }

        public async Task<IEnumerable<Transport>> GetTransports()
        {
            var transports = await _dbContext.Transports.ToListAsync();

            return transports;
        }

        public async Task<Transport> GetTransport(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Create(Transport dto)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Transport dto)
        {
            throw new NotImplementedException();
        }

    }
}
