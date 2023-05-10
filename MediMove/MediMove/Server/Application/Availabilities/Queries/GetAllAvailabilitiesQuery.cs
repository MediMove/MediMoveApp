using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Availabilities.Queries.GetAllAvailabilitiesQuery;

public record GetAllAvailabilitiesDTO : IRequest<ErrorOr<IEnumerable<AvailabilityDTO>>>;
public class GetAllAvailabilitiesQueryHandler : IRequestHandler<GetAllAvailabilitiesDTO, ErrorOr<IEnumerable<AvailabilityDTO>>>
{
    private readonly IMapper _mapper;
    private readonly IAvailabilityRepository _availabilityRepository;

    public GetAllAvailabilitiesQueryHandler(IMapper mapper, IAvailabilityRepository availabilityRepository)
    {
        _mapper = mapper;
        _availabilityRepository = availabilityRepository;
    }

    public async Task<ErrorOr<IEnumerable<AvailabilityDTO>>> Handle(GetAllAvailabilitiesDTO request, CancellationToken cancellationToken)
    {
        var availabilitys = await _availabilityRepository.GetAvailabilities();
        var availabilityDTOs = _mapper.Map<IEnumerable<AvailabilityDTO>>(availabilitys);

        if (availabilityDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(availabilityDTOs);
    }
}
