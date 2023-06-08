using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Application.Models;
using MediMove.Server.Data;

namespace MediMove.Server.Application.Availabilities.Handlers
{
    /// <summary>
    /// Handler for creating availabilities.
    /// </summary>
    public class CreateAvailabilitiesHandler : IRequestHandler<CreateAvailabilitiesCommand, ErrorOr<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for CreateAvailabilitiesHandler.
        /// </summary>
        /// <param name="mapper">mapper to inject</param>
        /// <param name="dbContext">dbContext to inject</param>
        public CreateAvailabilitiesHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the CreateAvailabilitiesCommand.
        /// </summary>
        /// <param name="request">CreateAvailabilitiesCommand</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Unit wrapped in ErrorOr</returns>
        public async Task<ErrorOr<Unit>> Handle(CreateAvailabilitiesCommand request, CancellationToken cancellationToken)
        {
            var availabilities = _mapper.Map<IEnumerable<Availability>>(request);

            if (availabilities is null)
                return Errors.Errors.MappingError;

            await _dbContext.Availabilities.AddRangeAsync(entities: availabilities, cancellationToken: cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ErrorOr<Unit>();
        }
    }
}
