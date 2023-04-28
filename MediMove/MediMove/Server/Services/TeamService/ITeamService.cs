﻿using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Services.TeamService
{
    public interface ITeamService
    {
        Task<TeamDTO> GetById(int id);
        Task<IEnumerable<TeamDTO>> GetAll();
        Task<IEnumerable<SelectTeamDTO>> GetFreeForStartDate(DateTime dt);
        int Create(CreateTeamDTO dto);
    }
}