using AutoMapper;
using MediMove.Server.Exceptions;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;
using System.Xml;

namespace MediMove.Server.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly ITransportRepository _transportRepository;
        private readonly IParamedicRepository _paramedicRepository;

        public TeamService(ITeamRepository teamRepository, IMapper mapper, ITransportRepository transportRepository, IParamedicRepository paramedicRepository)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _transportRepository = transportRepository;
            _paramedicRepository = paramedicRepository;
        }

        public async Task<TeamDTO> GetById(int id)
        {
            var team = await _teamRepository.GetTeam(id)
                ?? throw new EntityNotFoundException(typeof(Team), id);
            var teamDTO = _mapper.Map<TeamDTO>(team);

            return teamDTO;
        }
        public async Task<IEnumerable<TeamDTO>> GetAll()
        {
            var teams = await _teamRepository.GetTeams();
            var teamDTOs = _mapper.Map<IEnumerable<TeamDTO>>(teams);

            return teamDTOs;
        }

        public async Task<IEnumerable<SelectTeamDTO>> GetFreeForStartDate(DateTime dt)
        {
            var day = DateOnly.FromDateTime(dt);

            var teams = await _teamRepository.GetTeams();
            //var transports = await _transportRepository.GetTransportsForDay(day);

            /*var teamsX = from team in teams
                         join transport in transports on team.Id equals transport.TeamId
                        where team.Day.Day != day.Day || team.Day.Month != day.Month || team.Day.Year != day.Year
                        select team;
            */
            var teamsDTO = _mapper.Map<IEnumerable<SelectTeamDTO>>(teams);

            return teamsDTO;
        }

        public int Create(CreateTeamDTO dto)
        {
            if (dto.Day < DateOnly.FromDateTime(DateTime.Today))
                throw new ForeignKeyNotFoundException("Date cannot be in the past");

            var driver = _paramedicRepository.GetParamedic(dto.DriverId).Result
                ?? throw new ForeignKeyNotFoundException(typeof(Team), dto.DriverId, typeof(Paramedic));

            var paramedic = _paramedicRepository.GetParamedic(dto.ParamedicId).Result
                ?? throw new ForeignKeyNotFoundException(typeof(Team), dto.ParamedicId, typeof(Paramedic));

            var transports = (paramedic.IsDriver) ?
                _teamRepository.GetTeamsByDayAndDrivers(dto.Day, driver.Id, paramedic.Id) :
                _teamRepository.GetTeamsByDayAndParamedics(dto.Day, driver.Id, paramedic.Id);

            if (transports.Any())
                throw new NotImplementedException();

            var team = _mapper.Map<Team>(dto);

            _teamRepository.Update(team);
            return team.Id;
        }
    }
}
