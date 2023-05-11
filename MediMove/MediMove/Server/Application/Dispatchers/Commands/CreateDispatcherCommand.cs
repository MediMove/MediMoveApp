using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Dispatchers.Commands.CreateDispatcherCommand;

public class CreateDispatcherCommandHandler : IRequestHandler<CreateDispatcherDTO, ErrorOr<int>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public CreateDispatcherCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<int>> Handle(CreateDispatcherDTO request, CancellationToken cancellationToken)
    {
        var dispatcher = _mapper.Map<Dispatcher>(request);

        if (dispatcher is null)
            return Errors.Errors.MappingError;

        // TODO: Add validation

        await _dbContext.Dispatchers.AddAsync(dispatcher, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return dispatcher.Id;
    }
}
