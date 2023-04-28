using MediMove.Server.Models;

namespace MediMove.Server.Repositories.Contracts
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team?> GetTeam(int id);
        IEnumerable<Team> GetTeamsByDayAndDrivers(DateOnly day, int driverId, int paramedicId);
        IEnumerable<Team> GetTeamsByDayAndParamedics(DateOnly day, int driverId, int paramedicId);
        void Update(Team team);
    }
}
