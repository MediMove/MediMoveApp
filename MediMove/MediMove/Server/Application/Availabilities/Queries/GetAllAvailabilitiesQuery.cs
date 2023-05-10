using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs.temp;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Queries.GetAllAvailabilitiesQuery;

public record GetAllAvailabilitiesDTO : IRequest<ErrorOr<IEnumerable<AvailabilityDTO>>>;
public class GetAllAvailabilitiesQueryHandler : IRequestHandler<GetAllAvailabilitiesDTO, ErrorOr<IEnumerable<AvailabilityDTO>>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetAllAvailabilitiesQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<IEnumerable<AvailabilityDTO>>> Handle(GetAllAvailabilitiesDTO request, CancellationToken cancellationToken)
    {
        var availabilitys = await _dbContext.Availabilities.ToListAsync(cancellationToken: cancellationToken);

        var availabilityDTOs = _mapper.Map<IEnumerable<AvailabilityDTO>>(availabilitys);

        if (availabilityDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(availabilityDTOs);
    }
}
