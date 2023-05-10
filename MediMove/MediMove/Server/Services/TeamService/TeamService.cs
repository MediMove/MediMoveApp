/*
using AutoMapper;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;

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

        public async Task<IEnumerable<SelectTeamDTO>> GetAvailable(DateTime startTime, TransportType tt)
        {
            var day = DateOnly.FromDateTime(startTime);

            var teams = _teamRepository.GetTeamsByDayAndShift(startTime.ToDateOnly(), startTime.ToShiftType()
                ?? throw new NotImplementedException());
            DateTime start = startTime.AddHours(-1);
            DateTime end = startTime;
            int toAdd;
            if (tt == TransportType.Visit)
                toAdd = 2;
            else if (tt == TransportType.Handover)
                toAdd = 1;
            else
                throw new NotImplementedException();

            var conflictingTransports = await _transportRepository.GetTransportsByStartTimeRange(start, end.AddHours(toAdd));

            var result = teams
                .GroupJoin(_transportRepository.GetTransports().Result,
                    team => team.Id,
                    transport => transport.TeamId,
                    (team, transports) => new { Team = team, Transports = transports })
                .SelectMany(
                    x => x.Transports.DefaultIfEmpty(),
                    (teamInfo, transport) => new { Team = teamInfo.Team, Transport = transport })
                .Where(x => x.Team.Day.CompareToDateOnly(day) &&
                            (x.Transport == null || (x.Transport.StartTime < start && x.Transport.StartTime > end)))
                .Select(x => x.Team);
            
            var test = _teamRepository.GetTeams().Result
                .GroupJoin(_transportRepository.GetTransports().Result,
                    team => team.Id,
                    transport => transport.TeamId,
                    (team, transports) => new { Team = team, Transports = transports });

            var teamsDTO = _mapper.Map<IEnumerable<SelectTeamDTO>>(result);

            return teamsDTO;
        }

        public Team Create(CreateTeamDTO dto)
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
            return team;
        }
    }
}
*/