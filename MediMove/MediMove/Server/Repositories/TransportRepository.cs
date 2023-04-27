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

        private bool DateComapre(DateTime dateTime, DateOnly dateOnly)
        {
            return dateTime.Year == dateOnly.Year &&
                   dateTime.Month == dateOnly.Month &&
                   dateTime.Day == dateOnly.Day;
        }

        public async Task<IEnumerable<Transport>> GetByParamedicAndDay(int id, DateOnly date)
        {
            var transports = await _dbContext.Transports
                .Where(t => 
                    (t.Team.ParamedicId == id || t.Team.DriverId == id) &&
                    (
                        t.StartTime.Year == date.Year &&
                        t.StartTime.Month == date.Month &&
                        t.StartTime.Day == date.Day
                    ))
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .ToListAsync();

            
            return transports;
        }

        public async Task<IEnumerable<Transport>> GetTransportsForDay(DateOnly date)
        {
            var transports = await _dbContext.Transports
                .Where(t => (
                    t.StartTime.Year == date.Year &&
                    t.StartTime.Month == date.Month &&
                    t.StartTime.Day == date.Day
                ))
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .ToListAsync();

            return transports;
        }

        public async Task<IEnumerable<Transport>> GetTransports()
        {
            var transports = await _dbContext.Transports
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .ToListAsync();

            return transports;
        }

        public async Task<Transport> GetTransport(int id)
        {
            var transports = await _dbContext.Transports
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .FirstAsync(t => t.Id == id);

            return transports;
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
