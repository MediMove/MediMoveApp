using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;

namespace MediMove.Server.Application.Transports.Handlers
{
    public class UpdateTransportHandler : IRequestHandler<CreateTransportCommand, ErrorOr<Transport>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public UpdateTransportHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<Transport>> Handle(CreateTransportCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
