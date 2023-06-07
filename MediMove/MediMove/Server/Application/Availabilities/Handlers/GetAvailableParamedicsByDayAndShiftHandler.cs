using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Availabilities.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Handlers
{
    public class GetAvailableParamedicsByDayAndShiftHandler : IRequestHandler<GetAvailableParamedicsByDayAndShiftQuery, ErrorOr<GetAvailableParamedicsByDayAndShiftResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetAvailableParamedicsByDayAndShiftHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ErrorOr<GetAvailableParamedicsByDayAndShiftResponse>> Handle(GetAvailableParamedicsByDayAndShiftQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Availabilities
            .Where(d => d.Day.Date == request.Day.Date && d.ShiftType == request.Shift)
            .Include(a => a.Paramedic)
            .ThenInclude(p => p.PersonalInformation)
            .Where(p => p.Paramedic.IsWorking)
            .Select(p => new
            {
                p.Paramedic.Id,
                p.Paramedic.PersonalInformation.FirstName,
                p.Paramedic.PersonalInformation.LastName,
                p.Paramedic.IsDriver
            })
            .ToDictionaryAsync(
                result => result.Id,
                result => new GetAvailableParamedicsByDayAndShiftResponse.ParamedicInfo(
                    result.FirstName,
                    result.LastName,
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

            return ErrorOr.ErrorOr.From(response);
        }
    }
}