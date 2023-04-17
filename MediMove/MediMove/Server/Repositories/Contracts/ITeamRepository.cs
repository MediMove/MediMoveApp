﻿using MediMove.Shared.Entities;

namespace MediMove.Server.Repositories.Contracts
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeam(int id);
    }
}
