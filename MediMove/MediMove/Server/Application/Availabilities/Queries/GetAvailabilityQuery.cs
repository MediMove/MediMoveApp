using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Availabilities.Queries.GetAvailabilityQuery;

public record GetAvailabilityDTO(int Id) : IRequest<ErrorOr<AvailabilityDTO>>;
public class GetAvailabilityQueryHandler : IRequestHandler<GetAvailabilityDTO, ErrorOr<AvailabilityDTO>>
{
    private readonly IMapper _mapper;
    private readonly IAvailabilityRepository _availabilityRepository;

    public GetAvailabilityQueryHandler(IMapper mapper, IAvailabilityRepository availabilityRepository)
    {
        _mapper = mapper;
        _availabilityRepository = availabilityRepository;
    }

    public async Task<ErrorOr<AvailabilityDTO>> Handle(GetAvailabilityDTO request, CancellationToken cancellationToken)
    {
        var availability = await _availabilityRepository.GetAvailability(request.Id);

        if (availability is null)
            return Errors.Errors.EntityNotFoundError;

        var availabilityDTO = _mapper.Map<AvailabilityDTO>(availability);

        if (availabilityDTO is null)
            return Errors.Errors.MappingError;

        return availabilityDTO;
    }
}
