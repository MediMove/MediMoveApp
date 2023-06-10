using ErrorOr;
using MediatR;
using MediMove.Server.Application.Availabilities.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Handlers
{
    /// <summary>
    /// Handler for getting availabilities by paramedic.
    /// </summary>
    public class GetAvailabilitiesByParamedicQueryHandler : IRequestHandler<GetAvailabilitiesByParamedicQuery, ErrorOr<GetAvailabilitiesByParamedicResponse>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetAvailabilitiesByParamedicQueryHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public GetAvailabilitiesByParamedicQueryHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetAvailabilitiesByParamedicQuery.
        /// </summary>
        /// <param name="query">GetAvailabilitiesByParamedicQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetAvailabilitiesByParamedicResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetAvailabilitiesByParamedicResponse>> Handle(GetAvailabilitiesByParamedicQuery query, CancellationToken cancellationToken)
        {
            var availabilitiesDict = await _dbContext.Availabilities
                .Where(a => a.ParamedicId == query.ParamedicId &&
                            a.Day.Date >= DateTime.Today &&
                            a.Day.Date < DateTime.Today.AddMonths(1))
                .ToDictionaryAsync(a => a.Day.Date, a => a.ShiftType, cancellationToken);

            if (availabilitiesDict is null)
                return Errors.Errors.MappingError;

            return new GetAvailabilitiesByParamedicResponse(availabilitiesDict);
        }
    }
}