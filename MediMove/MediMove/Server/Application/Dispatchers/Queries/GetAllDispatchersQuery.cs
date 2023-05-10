using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Dispatchers.Queries.GetAllDispatchersQuery;

public record GetAllDispatchersDTO : IRequest<ErrorOr<IEnumerable<DispatcherDTO>>>;
public class GetAllDispatchersQueryHandler : IRequestHandler<GetAllDispatchersDTO, ErrorOr<IEnumerable<DispatcherDTO>>>
{
    private readonly IMapper _mapper;
    private readonly IDispatcherRepository _dispatcherRepository;

    public GetAllDispatchersQueryHandler(IMapper mapper, IDispatcherRepository dispatcherRepository)
    {
        _mapper = mapper;
        _dispatcherRepository = dispatcherRepository;
    }

    public async Task<ErrorOr<IEnumerable<DispatcherDTO>>> Handle(GetAllDispatchersDTO request, CancellationToken cancellationToken)
    {
        var dispatchers = await _dispatcherRepository.GetDispatchers();
        var dispatcherDTOs = _mapper.Map<IEnumerable<DispatcherDTO>>(dispatchers);

        if (dispatcherDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(dispatcherDTOs);
    }
}
