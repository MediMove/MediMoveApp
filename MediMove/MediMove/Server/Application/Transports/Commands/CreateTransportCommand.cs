using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Commands.CreateTransportCommand;

public class CreateTransportCommandHandler : IRequestHandler<CreateTransportDTO, ErrorOr<int>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public CreateTransportCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<int>> Handle(CreateTransportDTO request, CancellationToken cancellationToken)
    {
        var transport = _mapper.Map<Transport>(request);

        if (transport is null)
            return Errors.Errors.MappingError;

        // TODO: Add validation

        await _dbContext.Transports.AddAsync(transport, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return transport.Id;
    }
}

