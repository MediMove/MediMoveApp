using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Handlers
{
    /// <summary>
    /// Handler for getting transports by paramedic and date range.
    /// </summary>
    public class GetTransportsByParamedicAndDateRangeQueryHandler : IRequestHandler<GetTransportsByParamedicAndDateRangeQuery, ErrorOr<GetTransportsByParamedicAndDateRangeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetTransportsByParamedicAndDateRangeQueryHandler.
        /// </summary>
        /// <param name="mapper">mapper to inject</param>
        /// <param name="dbContext">dbContext to inject</param>
        public GetTransportsByParamedicAndDateRangeQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetTransportsByParamedicAndDateRangeQuery.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ErrorOr<GetTransportsByParamedicAndDateRangeResponse>> Handle(GetTransportsByParamedicAndDateRangeQuery request, CancellationToken cancellationToken)
        {
            var transports = await _dbContext.Transports
                .Where(t => !t.IsCancelled &&
                    (!request.StartDateInclusive.HasValue || t.StartTime.Date >= request.StartDateInclusive.Value.Date) &&
                    (!request.EndDateInclusive.HasValue || t.StartTime.Date <= request.EndDateInclusive.Value.Date) &&
                    (t.Team.DriverId == request.ParamedicId || t.Team.ParamedicId == request.ParamedicId))
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .ToArrayAsync(cancellationToken);

            var transportDTOs = _mapper.Map<GetTransportResponse[]>(transports);

            if (transportDTOs is null)
                return Errors.Errors.MappingError;

            return new GetTransportsByParamedicAndDateRangeResponse(transportDTOs);
        }
    }
}