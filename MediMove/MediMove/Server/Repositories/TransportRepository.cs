﻿using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
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

        public async Task<IEnumerable<Transport>> GetByParamedicAndDay(int id, DateOnly date)
        {
            var transports = await _dbContext.Transports
                .Where(t => 
                    (t.Team.ParamedicId == id || t.Team.DriverId == id) &&
                    DateOnly.FromDateTime(t.StartTime.Date) == date)
                .ToListAsync();

            return transports;
        }


        public async Task<IEnumerable<Transport>> GetTransportsForDay(DateOnly date)
        {
            var transports = await _dbContext.Transports
                .Where(t =>
                    DateOnly.FromDateTime(t.StartTime) == date)
                .ToListAsync();
            return transports;
        }

        public async Task<IEnumerable<Transport>> GetTransportsForDate(DateTime date)
        {
            var transports = from transport in _dbContext.Transports
                             where (transport.TransportType == Shared.Models.Enums.TransportType.Handover && transport.StartTime >= date.AddMinutes(30) &&
                             transport.StartTime <= date.AddMinutes(30)) ||
                             (transport.TransportType == Shared.Models.Enums.TransportType.Visit && transport.StartTime >= date.AddMinutes(30) &&
                             transport.StartTime <= date.AddMinutes(30))
                             select transport;

            return transports;
        }

        public async Task<IEnumerable<Transport>> GetTransportsByStartTimeRange(DateTime start, DateTime end)
        {
            var transports = from transport in _dbContext.Transports
                             where transport.StartTime >= start &&
                             transport.StartTime <= end
                             select transport;

            return transports;
        }


        public async Task<IEnumerable<Transport>> GetTransports()
        {
            var transports = await _dbContext.Transports.ToListAsync();

            return transports;
        }

        public async Task<Transport?> GetTransport(int id)
        {
            return await _dbContext.Transports.FindAsync(id);
        }

        public async Task<IEnumerable<Transport>> GetTransports(Transport dto)
        {
            return await _dbContext.Transports.ToListAsync();
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
