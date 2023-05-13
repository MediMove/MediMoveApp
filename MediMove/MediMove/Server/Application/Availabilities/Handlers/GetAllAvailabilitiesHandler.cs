using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Availabilities.Queries;
using MediMove.Server.Data;
using MediMove.Server.Errors;
using MediMove.Shared.Models.DTOs.V2;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Handlers
{
    public class GetAllAvailabilitiesHandler : IRequestHandler<GetAllAvailabilitiesQuery, ErrorOr<IEnumerable<AvailabilityDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetAllAvailabilitiesHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ErrorOr<IEnumerable<AvailabilityDTO>>> Handle(GetAllAvailabilitiesQuery request, CancellationToken cancellationToken)
        {
            var availabilities = await _dbContext.Availabilities
                .Where( d => d.Day.Date == request.Day.Date)
                .Include(a => a.Paramedic)
                .ThenInclude(p => p.PersonalInformation)
                .ToListAsync(cancellationToken: cancellationToken);

            var availabilitiesDTO = _mapper.Map<IEnumerable<AvailabilityDTO>>(availabilities);

            if (availabilitiesDTO is null)
                return Errors.Errors.MappingError;

            return ErrorOr.ErrorOr.From(availabilitiesDTO);
        }
    }
}