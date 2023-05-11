using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.Enums;
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

        public async Task<Team?> GetTeam(int id)
        {
            return await _dbContext.Teams.FindAsync(id);
        }

        public IEnumerable<Team> GetTeamsByDay(DateOnly day)
        {
            var teams = from team in _dbContext.Teams
                        where team.Day.Day == day.Day &&
                        team.Day.Month == day.Month &&
                        team.Day.Year == day.Year
                        select team;
            return teams;
        }

        public IEnumerable<Team> GetTeamsByDayAndShift(DateOnly day, ShiftType st)
        {
            var teams = from team in _dbContext.Teams
                        where team.Day.Day == day.Day &&
                        team.Day.Month == day.Month &&
                        team.Day.Year == day.Year
                        select team;
            return teams;
        }

        public IEnumerable<Team> GetTeamsByDayAndDrivers(DateOnly day, int driverId, int paramedicId)
        {
            var teams = from team in _dbContext.Teams
                        where team.Day.Year == day.Year &&
                        team.Day.Month == day.Month &&
                        team.Day.Day == day.Day && (
                        team.DriverId == driverId ||
                        team.ParamedicId == driverId ||
                        team.DriverId == paramedicId ||
                        team.ParamedicId == paramedicId)
                        select team;
            return teams;
        }

        public IEnumerable<Team> GetTeamsByDayAndParamedics(DateOnly day, int driverId, int paramedicId)
        {
            var teams = from team in _dbContext.Teams
                        where team.Day.Year == day.Year &&
                        team.Day.Month == day.Month &&
                        team.Day.Day == day.Day && (
                        team.DriverId == driverId ||
                        team.ParamedicId == driverId ||
                        team.ParamedicId == paramedicId)
                        select team;
            return teams;
        }


        public void Update(Team team)
        {
            _dbContext.Teams.Update(team);
            _dbContext.SaveChanges();
        }

        /*
        public async Task<IEnumerable<Team>> GetByDay(DateOnly day)
        {
            

            return teams;
        }
        */
    }
}
