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
            var paramedics = await _dbContext.Availabilities
                .Where(a => a.Day.Date == request.Date.Date &&
                    (!a.ShiftType.HasValue || a.ShiftType == request.Shift) &&
                    a.Paramedic.IsWorking)
                .Include(a => a.Paramedic)
                .ThenInclude(p => p.PersonalInformation)
                .Select(a => new
                {
                    a.ParamedicId,
                    ParamedicInfo = new GetAvailableParamedicsByDateAndShiftResponse.ParamedicInfo(
                        a.Paramedic.PersonalInformation.FirstName,
                        a.Paramedic.PersonalInformation.LastName,
                        a.Paramedic.PersonalInformation.PhoneNumber,
                        a.Paramedic.IsDriver
                    )
                })
                .ToDictionaryAsync(
                    p => p.ParamedicId,
                    p => p.ParamedicInfo,
                    cancellationToken
                );

            if (paramedics is null)
                return Errors.Errors.MappingError;

            return new GetAvailableParamedicsByDateAndShiftResponse
            {
                Paramedics = paramedics
            };
        }
    }
}