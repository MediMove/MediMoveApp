using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Availabilities.Commands.CreateAvailabilitiesCommand;

public class CreateAvailabilitiesCommandHandler : IRequestHandler<CreateAvailabilitiesDTO, ErrorOr<Unit>>
{
    private readonly IMapper _mapper;
    private readonly IAvailabilityRepository _availabilitiesRepository;

    public CreateAvailabilitiesCommandHandler(IMapper mapper, IAvailabilityRepository availabilitiesRepository)
    {
        _mapper = mapper;
        _availabilitiesRepository = availabilitiesRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(CreateAvailabilitiesDTO request, CancellationToken cancellationToken)
    {
        var availabilities = _mapper.Map<IEnumerable<Availability>>(request);

        if (availabilities is null)
            return Errors.Errors.MappingError;

        throw new NotImplementedException("Validation not implemented");
        // TODO: Add availabilities to database
        return new ErrorOr<Unit>();
    }
}