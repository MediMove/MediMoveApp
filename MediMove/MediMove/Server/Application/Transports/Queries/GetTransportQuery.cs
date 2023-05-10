using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Queries.GetTransportQuery;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Queries.GetTransportQuery;

public record GetTransportDTO(int Id) : IRequest<ErrorOr<TransportDTO>>;
public class GetTransportQueryHandler : IRequestHandler<GetTransportDTO, ErrorOr<TransportDTO>>
{
    private readonly IMapper _mapper;
    private readonly ITransportRepository _transportRepository;

    public GetTransportQueryHandler(IMapper mapper, ITransportRepository transportRepository)
    {
        _mapper = mapper;
        _transportRepository = transportRepository;
    }

    public async Task<ErrorOr<TransportDTO>> Handle(GetTransportDTO request, CancellationToken cancellationToken)
    {
        var transport = await _transportRepository.GetTransport(request.Id);

        if (transport is null)
            return Errors.Errors.EntityNotFoundError;

        var transportDTO = _mapper.Map<TransportDTO>(transport);

        if (transportDTO is null)
            return Errors.Errors.MappingError;

        return transportDTO;
    }
}
