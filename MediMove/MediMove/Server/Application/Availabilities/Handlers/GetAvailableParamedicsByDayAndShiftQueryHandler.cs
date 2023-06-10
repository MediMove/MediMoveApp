using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Availabilities.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Handlers
{
    /// <summary>
    /// Handler for getting available paramedics by day and shift.
    /// </summary>
    public class GetAvailableParamedicsByDayAndShiftQueryHandler : IRequestHandler<GetAvailableParamedicsByDayAndShiftQuery, ErrorOr<GetAvailableParamedicsByDayAndShiftResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetAvailableParamedicsByDayAndShiftQueryHandler.
        /// </summary>
        /// <param name="mapper">mapper to inject</param>
        /// <param name="dbContext">dbContext to inject</param>
        public GetAvailableParamedicsByDayAndShiftQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetAvailableParamedicsByDayAndShiftQuery.
        /// </summary>
        /// <param name="request">GetAvailableParamedicsByDayAndShiftQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetAvailableParamedicsByDayAndShiftResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetAvailableParamedicsByDayAndShiftResponse>> Handle(GetAvailableParamedicsByDayAndShiftQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Availabilities
                .Where(a => a.Day.Date == request.Day.Date &&
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
                    result => new GetAvailableParamedicsByDayAndShiftResponse.ParamedicInfo(
                        result.FirstName,
                        result.LastName,
                        result.PhoneNumber,
                        result.IsDriver
                    ),
                    cancellationToken
                );

            if (query is null)
                return Errors.Errors.MappingError;

            var response = new GetAvailableParamedicsByDayAndShiftResponse
            {
                Paramedics = query
            };

            return response;
        }
    }
}