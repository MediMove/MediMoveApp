using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;

namespace MediMove.Server.Application.Transports.Handlers
{
    public class CreateTransportCommandHandler : IRequestHandler<CreateTransportCommand, ErrorOr<Transport>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public CreateTransportCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<Transport>> Handle(CreateTransportCommand request, CancellationToken cancellationToken)
        {
            var transport = _mapper.Map<Transport>(request.Dto);

            if (transport is null)
                return Errors.Errors.MappingError;

            await _dbContext.Transports.AddAsync(transport, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return transport;
        }
    }

}
