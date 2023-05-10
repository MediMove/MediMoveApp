using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Commands.CreateTransportCommand;

public class CreateTransportCommandHandler : IRequestHandler<CreateTransportDTO, ErrorOr<int>>
{
    private readonly IMapper _mapper;
    private readonly ITransportRepository _transportRepository;

    public CreateTransportCommandHandler(IMapper mapper, ITransportRepository transportRepository)
    {
        _mapper = mapper;
        _transportRepository = transportRepository;
    }

    public async Task<ErrorOr<int>> Handle(CreateTransportDTO request, CancellationToken cancellationToken)
    {
        var transport = _mapper.Map<Transport>(request);

        if (transport is null)
            return Errors.Errors.MappingError;

        throw new NotImplementedException("Validation not implemented");
        // TODO: Add transport to database

        return transport.Id;
    }
}

