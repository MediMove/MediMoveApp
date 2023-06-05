using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Data;
using MediMove.Server.Models;


namespace MediMove.Server.Application.Availabilities.Handlers
{
    public class CreateAvailabilitiesHandler : IRequestHandler<CreateAvailabilitiesCommand, ErrorOr<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public CreateAvailabilitiesHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateAvailabilitiesCommand request, CancellationToken cancellationToken)
        {
            var availabilities = _mapper.Map<IEnumerable<Availability>>(request.Dto);

            if (availabilities is null)
                return Errors.Errors.MappingError;

            await _dbContext.Availabilities.AddRangeAsync(entities: availabilities,cancellationToken: cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ErrorOr<Unit>();
        }

    }
}
