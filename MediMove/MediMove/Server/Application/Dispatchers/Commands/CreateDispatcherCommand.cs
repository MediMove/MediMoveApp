using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Dispatchers.Commands.CreateDispatcherCommand;

public class CreateDispatcherCommandHandler : IRequestHandler<CreateDispatcherDTO, ErrorOr<int>>
{
    private readonly IMapper _mapper;
    private readonly IDispatcherRepository _dispatcherRepository;

    public CreateDispatcherCommandHandler(IMapper mapper, IDispatcherRepository dispatcherRepository)
    {
        _mapper = mapper;
        _dispatcherRepository = dispatcherRepository;
    }

    public async Task<ErrorOr<int>> Handle(CreateDispatcherDTO request, CancellationToken cancellationToken)
    {
        var dispatcher = _mapper.Map<Dispatcher>(request);

        if (dispatcher is null)
            return Errors.Errors.MappingError;

        throw new NotImplementedException("Validation not implemented");
        // TODO: Add dispatcher to database

        return dispatcher.Id;
    }
}
