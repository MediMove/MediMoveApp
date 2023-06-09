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
    public class GetTeamsByDayAndShiftHandler : IRequestHandler<GetTeamsByDayAndShiftQuery, ErrorOr<GetTeamsByDayAndShiftResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetTeamsByDayAndShiftHandler.
        /// </summary>
        /// <param name="mapper">mapper to inject</param>
        /// <param name="dbContext">dbContext to inject</param>
        public GetTeamsByDayAndShiftHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetTeamsByDayAndShiftQuery.
        /// </summary>
        /// <param name="request">GetTeamsByDayAndShiftQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetTeamsByDayAndShiftResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetTeamsByDayAndShiftResponse>> Handle(GetTeamsByDayAndShiftQuery request, CancellationToken cancellationToken)
        {
            var dict = await _dbContext.Teams
                .Where(t => t.Day.Date == request.Day.Date)
                .Include(t => t.Driver)
                    .ThenInclude(p => p.PersonalInformation)
                .Include(t => t.Driver)
                    .ThenInclude(p => p.Availabilities)
                .Include(t => t.Paramedic)
                    .ThenInclude(p => p.PersonalInformation)
                .Where(t => t.Driver.Availabilities
                    .Any(a => a.Day.Date == request.Day.Date
                        && a.ShiftType == request.Shift))
                .Select(t => new
                {
                    t.Id,
                    TeamInfo = _mapper.Map<GetTeamsByDayAndShiftResponse.TeamInfo>(t)
                })
                .ToDictionaryAsync(
                    keySelector: t => t.Id,
                    elementSelector: t => t.TeamInfo,
                    cancellationToken);

            if (dict is null)
                return Errors.Errors.MappingError;

            var response = new GetTeamsByDayAndShiftResponse
            {
                Teams = dict
            };

            return response;
        }
    }
}
