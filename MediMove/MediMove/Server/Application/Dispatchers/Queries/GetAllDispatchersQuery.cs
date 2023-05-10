using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs.temp;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Dispatchers.Queries.GetAllDispatchersQuery;

public record GetAllDispatchersDTO : IRequest<ErrorOr<IEnumerable<DispatcherDTO>>>;
public class GetAllDispatchersQueryHandler : IRequestHandler<GetAllDispatchersDTO, ErrorOr<IEnumerable<DispatcherDTO>>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetAllDispatchersQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<IEnumerable<DispatcherDTO>>> Handle(GetAllDispatchersDTO request, CancellationToken cancellationToken)
    {
        var dispatchers = await _dbContext.Dispatchers.ToListAsync(cancellationToken: cancellationToken);

        var dispatcherDTOs = _mapper.Map<IEnumerable<DispatcherDTO>>(dispatchers);

        if (dispatcherDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(dispatcherDTOs);
    }
}
