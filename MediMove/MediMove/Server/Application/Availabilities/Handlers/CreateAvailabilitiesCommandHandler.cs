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
    public class CreateAvailabilitiesCommandHandler : IRequestHandler<CreateAvailabilitiesCommand, ErrorOr<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for CreateAvailabilitiesCommandHandler.
        /// </summary>
        /// <param name="mapper">mapper to inject</param>
        /// <param name="dbContext">dbContext to inject</param>
        public CreateAvailabilitiesCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the CreateAvailabilitiesCommand.
        /// </summary>
        /// <param name="command">CreateAvailabilitiesCommand</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Unit wrapped in ErrorOr</returns>
        public async Task<ErrorOr<Unit>> Handle(CreateAvailabilitiesCommand command, CancellationToken cancellationToken)
        {
            var availabilities = _mapper.Map<Availability[]>(command);

            if (availabilities is null)
                return Errors.Errors.MappingError;

            await _dbContext.Availabilities.AddRangeAsync(availabilities, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ErrorOr<Unit>();
        }
    }
}
