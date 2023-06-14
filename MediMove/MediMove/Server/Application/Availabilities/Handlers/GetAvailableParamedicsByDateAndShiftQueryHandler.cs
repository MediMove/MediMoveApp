using ErrorOr;
using MediatR;
using MediMove.Server.Application.Availabilities.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Handlers
{
    /// <summary>
    /// Handler for getting available paramedics by date and shift.
    /// </summary>
    public class GetAvailableParamedicsByDateAndShiftQueryHandler : IRequestHandler<GetAvailableParamedicsByDateAndShiftQuery, ErrorOr<GetAvailableParamedicsByDateAndShiftResponse>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetAvailableParamedicsByDateAndShiftQueryHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public GetAvailableParamedicsByDateAndShiftQueryHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetAvailableParamedicsByDateAndShiftQuery.
        /// </summary>
        /// <param name="request">GetAvailableParamedicsByDateAndShiftQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetAvailableParamedicsByDateAndShiftResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetAvailableParamedicsByDateAndShiftResponse>> Handle(GetAvailableParamedicsByDateAndShiftQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Availabilities
                .Where(a => a.Day.Date == request.Date.Date &&
                    (a.ShiftType == null || a.ShiftType == request.Shift))
                .Include(a => a.Paramedic)
                .ThenInclude(p => p.PersonalInformation)
                .Where(p => p.Paramedic.IsWorking)
                .Select(p => new
                {
                    p.Paramedic.Id,
                    p.Paramedic.PersonalInformation.FirstName,
                    p.Paramedic.PersonalInformation.LastName,
                    p.Paramedic.PersonalInformation.PhoneNumber,
                    p.Paramedic.IsDriver
                })
                .ToDictionaryAsync(
                    result => result.Id,
                    result => new GetAvailableParamedicsByDateAndShiftResponse.ParamedicInfo(
                        result.FirstName,
                        result.LastName,
                        result.PhoneNumber,
                        result.IsDriver
                    ),
                    cancellationToken
                );

            if (query is null)
                return Errors.Errors.MappingError;

            return new GetAvailableParamedicsByDateAndShiftResponse
            {
                Paramedics = query
            };
        }
    }
}