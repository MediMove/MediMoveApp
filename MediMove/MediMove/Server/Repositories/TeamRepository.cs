﻿using MediMove.Server.Data;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly MediMoveDbContext _dbContext;

        public TeamRepository(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _dbContext.Teams.ToListAsync();
        }

        public Task<Team> GetTeam(int id)
        {
            throw new NotImplementedException();
        }
    }
}
