using MediMove.Server.Models;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Repositories.Contracts
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team?> GetTeam(int id);
        IEnumerable<Team> GetTeamsByDayAndDrivers(DateOnly day, int driverId, int paramedicId);
        IEnumerable<Team> GetTeamsByDayAndParamedics(DateOnly day, int driverId, int paramedicId);
        void Update(Team team);
        IEnumerable<Team> GetTeamsByDay(DateOnly day);
        IEnumerable<Team> GetTeamsByDayAndShift(DateOnly day, ShiftType st);
    }
}
