using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Availabilities.Queries.GetAvailabilityQuery;

public record GetAvailabilityDTO(int Id) : IRequest<ErrorOr<AvailabilityDTO>>;
public class GetAvailabilityQueryHandler : IRequestHandler<GetAvailabilityDTO, ErrorOr<AvailabilityDTO>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetAvailabilityQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<AvailabilityDTO>> Handle(GetAvailabilityDTO request, CancellationToken cancellationToken)
    {
        var availability = await _dbContext.Availabilities.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (availability is null)
            return Errors.Errors.EntityNotFoundError;

        var availabilityDTO = _mapper.Map<AvailabilityDTO>(availability);

        if (availabilityDTO is null)
            return Errors.Errors.MappingError;

        return availabilityDTO;
    }
}
