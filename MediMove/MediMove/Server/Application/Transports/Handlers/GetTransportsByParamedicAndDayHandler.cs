using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MediMove.Server.Application.Transports.Handlers
{
    public class GetTransportsByParamedicAndDayHandler : IRequestHandler<GetTransportsByParamedicAndDayQuery, ErrorOr<GetTransportsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetTransportsByParamedicAndDayHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<GetTransportsResponse>> Handle(GetTransportsByParamedicAndDayQuery request, CancellationToken cancellationToken)
        {
            var dateOnly = request.Day;
            var query = await _dbContext.Transports
                .Where(t => t.StartTime.Date == request.Day.Date)
                .Where(t => t.Team.DriverId == request.ParamedicId || t.Team.ParamedicId == request.ParamedicId)
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .Select(t => new
                {
                    t.Id,
                    GetTransportResponse = _mapper.Map<GetTransportResponse>(t)
                })
                .ToDictionaryAsync(
                    keySelector: t => t.Id,
                    elementSelector: t => t.GetTransportResponse, cancellationToken
                );


            if (query is null)
                return Errors.Errors.MappingError;

            var response = new GetTransportsResponse
            {
                Transports = query
            };

            return ErrorOr.ErrorOr.From(response);
        }
    }
}
