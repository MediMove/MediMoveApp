using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Queries.GetTransportQuery;

public record GetTransportDTO(int Id) : IRequest<ErrorOr<TransportDTO>>;
public class GetTransportQueryHandler : IRequestHandler<GetTransportDTO, ErrorOr<TransportDTO>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetTransportQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<TransportDTO>> Handle(GetTransportDTO request, CancellationToken cancellationToken)
    {
        var transport = await _dbContext.Transports.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (transport is null)
            return Errors.Errors.EntityNotFoundError;

        var transportDTO = _mapper.Map<TransportDTO>(transport);

        if (transportDTO is null)
            return Errors.Errors.MappingError;

        return transportDTO;
    }
}
