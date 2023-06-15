using ErrorOr;
using MediatR;
using MediMove.Server.Application.Teams.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Teams.Handlers
{
    /// <summary>
    /// Handler for getting teams by date and shift.
    /// </summary>
    public class GetTeamsByDateAndShiftQueryHandler : IRequestHandler<GetTeamsByDateAndShiftQuery, ErrorOr<GetTeamsByDateAndShiftResponse>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetTeamsByDateAndShiftQueryHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public GetTeamsByDateAndShiftQueryHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetTeamsByDateAndShiftQuery.
        /// </summary>
        /// <param name="query">GetTeamsByDateAndShiftQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetTeamsByDateAndShiftResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetTeamsByDateAndShiftResponse>> Handle(GetTeamsByDateAndShiftQuery query, CancellationToken cancellationToken)
        {
            var teamsDict = await _dbContext.Teams
                .Where(t => t.Day.Date == query.Date.Date &&
                    t.ShiftType == query.Shift)
                .Select(t => new
                {
                    t.Id,
                    TeamInfo = new GetTeamsByDateAndShiftResponse.TeamInfo
                    (
                        t.DriverId,
                        t.Driver.PersonalInformation.FirstName,
                        t.Driver.PersonalInformation.LastName,
                        t.Driver.PersonalInformation.PhoneNumber,

                        t.ParamedicId,
                        t.Paramedic.PersonalInformation.FirstName,
                        t.Paramedic.PersonalInformation.LastName,
                        t.Paramedic.PersonalInformation.PhoneNumber
                    )
                })
                .ToDictionaryAsync(
                    keySelector: t => t.Id,
                    elementSelector: t => t.TeamInfo,
                    cancellationToken);

            if (teamsDict is null)
                return Errors.Errors.MappingError;

            return new GetTeamsByDateAndShiftResponse
            {
                Teams = teamsDict
            };
        }
    }
}