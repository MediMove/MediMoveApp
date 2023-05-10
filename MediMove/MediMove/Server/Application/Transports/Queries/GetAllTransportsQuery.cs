using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Teams.Queries.GetTeamsQuery;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Queries.GetAllTransportsQuery;

public record GetAllTransportsDTO : IRequest<ErrorOr<IEnumerable<TransportDTO>>>;
public class GetAllTransportsQueryHandler : IRequestHandler<GetAllTransportsDTO, ErrorOr<IEnumerable<TransportDTO>>>
{
    private readonly IMapper _mapper;
    private readonly ITransportRepository _transportRepository;

    public GetAllTransportsQueryHandler(IMapper mapper, ITransportRepository transportRepository)
    {
        _mapper = mapper;
        _transportRepository = transportRepository;
    }

    public async Task<ErrorOr<IEnumerable<TransportDTO>>> Handle(GetAllTransportsDTO request, CancellationToken cancellationToken)
    {
        var transports = await _transportRepository.GetTransports();
        var transportDTOs = _mapper.Map<IEnumerable<TransportDTO>>(transports);

        if (transportDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(transportDTOs);
    }
}
