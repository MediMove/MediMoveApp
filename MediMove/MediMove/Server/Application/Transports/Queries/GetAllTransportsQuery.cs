using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Queries.GetAllTransportsQuery;

public record GetAllTransportsDTO : IRequest<ErrorOr<IEnumerable<TransportDTO>>>;
public class GetAllTransportsQueryHandler : IRequestHandler<GetAllTransportsDTO, ErrorOr<IEnumerable<TransportDTO>>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetAllTransportsQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<IEnumerable<TransportDTO>>> Handle(GetAllTransportsDTO request, CancellationToken cancellationToken)
    {
        var transports = await _dbContext.Transports.ToListAsync(cancellationToken: cancellationToken);

        var transportDTOs = _mapper.Map<IEnumerable<TransportDTO>>(transports);

        if (transportDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(transportDTOs);
    }
}
