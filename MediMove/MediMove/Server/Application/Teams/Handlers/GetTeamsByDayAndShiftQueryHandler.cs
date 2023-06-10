using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Teams.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Teams.Handlers
{
    /// <summary>
    /// Handler for getting teams by day and shift.
    /// </summary>
    public class GetTeamsByDayAndShiftQueryHandler : IRequestHandler<GetTeamsByDayAndShiftQuery, ErrorOr<GetTeamsByDayAndShiftResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetTeamsByDayAndShiftHandler.
        /// </summary>
        /// <param name="mapper">mapper to inject</param>
        /// <param name="dbContext">dbContext to inject</param>
        public GetTeamsByDayAndShiftQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetTeamsByDayAndShiftQuery.
        /// </summary>
        /// <param name="query">GetTeamsByDayAndShiftQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetTeamsByDayAndShiftResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetTeamsByDayAndShiftResponse>> Handle(GetTeamsByDayAndShiftQuery query, CancellationToken cancellationToken)
        {
            var teamsDict = await _dbContext.Teams
                .Where(t => t.Day.Date == query.Day.Date &&
                    t.ShiftType == query.Shift)
                .Include(t => t.Driver)
                    .ThenInclude(p => p.PersonalInformation)
                .Include(t => t.Driver)
                    .ThenInclude(p => p.Availabilities)
                .Include(t => t.Paramedic)
                    .ThenInclude(p => p.PersonalInformation)
                .Select(t => new
                {
                    t.Id,
                    TeamInfo = new GetTeamsByDayAndShiftResponse.TeamInfo
                    {
                        DriverId = t.Driver.Id,
                        DriverFirstName = t.Driver.PersonalInformation.FirstName,
                        DriverLastName = t.Driver.PersonalInformation.LastName,
                        DriverPhoneNumber = t.Driver.PersonalInformation.PhoneNumber,

                        ParamedicId = t.Paramedic.Id,
                        ParamedicFirstName = t.Paramedic.PersonalInformation.FirstName,
                        ParamedicLastName = t.Paramedic.PersonalInformation.LastName,
                        ParamedicPhoneNumber = t.Paramedic.PersonalInformation.PhoneNumber,
                    }
                })
                .ToDictionaryAsync(
                    keySelector: t => t.Id,
                    elementSelector: t => t.TeamInfo,
                    cancellationToken);

            if (teamsDict is null)
                return Errors.Errors.MappingError;

            var response = new GetTeamsByDayAndShiftResponse
            {
                Teams = teamsDict
            };

            return response;
        }
    }
}
