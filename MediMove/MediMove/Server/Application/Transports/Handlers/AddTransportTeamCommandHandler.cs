using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using MediMove.Server.Models;

namespace MediMove.Server.Application.Transports.Handlers
{
    public class AddTransportTeamCommandHandler : IRequestHandler<AddTransportTeamCommand, ErrorOr<Transport>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public AddTransportTeamCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ErrorOr<Transport>> Handle(AddTransportTeamCommand request, CancellationToken cancellationToken)
        {
            var transport = _dbContext.Transports.FirstOrDefault(t => t.Id == request.TransportId);
            if(transport is null) return Errors.Errors.EntityNotFoundError;

            transport.TeamId = request.TeamId;

            await _dbContext.Transports.AddAsync(transport, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return transport;
        }
    }
}
