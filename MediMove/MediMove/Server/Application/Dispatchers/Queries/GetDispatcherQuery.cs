using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Dispatchers.Queries.GetDispatcherQuery;

public record GetDispatcherDTO(int Id) : IRequest<ErrorOr<DispatcherDTO>>;
public class GetDispatcherQueryHandler : IRequestHandler<GetDispatcherDTO, ErrorOr<DispatcherDTO>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetDispatcherQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<DispatcherDTO>> Handle(GetDispatcherDTO request, CancellationToken cancellationToken)
    {
        var dispatcher = await _dbContext.Dispatchers.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (dispatcher is null)
            return Errors.Errors.EntityNotFoundError;

        var dispatcherDTO = _mapper.Map<DispatcherDTO>(dispatcher);

        if (dispatcherDTO is null)
            return Errors.Errors.MappingError;

        return dispatcherDTO;
    }
}