using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Server.Data;
using MediMove.Server.Errors;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Handlers
{
    public class GetTransportsByParamedicAndDayHandler : IRequestHandler<GetTransportsByParamedicAndDayQuery, ErrorOr<IEnumerable<TransportDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetTransportsByParamedicAndDayHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<IEnumerable<TransportDTO>>> Handle(GetTransportsByParamedicAndDayQuery request, CancellationToken cancellationToken)
        {
            var dateOnly = request.Day;
            var transports = await _dbContext.Transports
                    .Where(t => t.StartTime.Date == request.Day.Date)
                    .Where(t => t.Team.DriverId == request.ParamedicId || t.Team.ParamedicId == request.ParamedicId)
                    .Include(t => t.Patient)
                    .ThenInclude(p => p.PersonalInformation)
                    .ToListAsync(cancellationToken: cancellationToken);

            var transportDTOs = _mapper.Map<IEnumerable<TransportDTO>>(transports);

            if (transportDTOs is null)
                return Errors.Errors.MappingError;

            return ErrorOr.ErrorOr.From(transportDTOs);
        }
    }
}
