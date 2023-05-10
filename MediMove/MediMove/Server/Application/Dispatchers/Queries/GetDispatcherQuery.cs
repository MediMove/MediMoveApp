using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Dispatchers.Queries.GetDispatcherQuery;

public record GetDispatcherDTO(int Id) : IRequest<ErrorOr<DispatcherDTO>>;
public class GetDispatcherQueryHandler : IRequestHandler<GetDispatcherDTO, ErrorOr<DispatcherDTO>>
{
    private readonly IMapper _mapper;
    private readonly IDispatcherRepository _dispatcherRepository;

    public GetDispatcherQueryHandler(IMapper mapper, IDispatcherRepository dispatcherRepository)
    {
        _mapper = mapper;
        _dispatcherRepository = dispatcherRepository;
    }

    public async Task<ErrorOr<DispatcherDTO>> Handle(GetDispatcherDTO request, CancellationToken cancellationToken)
    {
        var dispatcher = await _dispatcherRepository.GetDispatcher(request.Id);

        if (dispatcher is null)
            return Errors.Errors.EntityNotFoundError;

        var dispatcherDTO = _mapper.Map<DispatcherDTO>(dispatcher);

        if (dispatcherDTO is null)
            return Errors.Errors.MappingError;

        return dispatcherDTO;
    }
}