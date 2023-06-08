using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Handlers
{
    public class GetTransportQueryHandler : IRequestHandler<GetTransportQuery, ErrorOr<GetTransportResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetTransportQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<GetTransportResponse>> Handle(GetTransportQuery request, CancellationToken cancellationToken)
        {
            var transport = await _dbContext.Transports.FindAsync(request.Id, cancellationToken);

            if (transport is null)
                return Errors.Errors.EntityNotFoundError;

            var transportDTO = _mapper.Map<GetTransportResponse>(transport);

            if (transportDTO is null)
                return Errors.Errors.MappingError;

            return transportDTO;
        }
    }
}