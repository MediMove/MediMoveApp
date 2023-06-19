using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Server.Data;

namespace MediMove.Server.Application.Teams.Handlers
{
    /// <summary>
    /// Handler for creating teams.
    /// </summary>
    public class CreateTeamsCommandHandler : IRequestHandler<CreateTeamsCommand, ErrorOr<Team[]>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for CreateTeamsCommandHandler.
        /// </summary>
        /// <param name="mapper">mapper to inject</param>
        /// <param name="dbContext">dbContext to inject</param>
        public CreateTeamsCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the CreateTeamsCommand.
        /// </summary>
        /// <param name="command">CreateTeamsCommand</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>IEnumerable of Teams wrapped in ErrorOr</returns>
        public async Task<ErrorOr<Team[]>> Handle(CreateTeamsCommand command, CancellationToken cancellationToken)
        {
            var teams = _mapper.Map<Team[]>(command);

            if (teams is null)
                return Errors.Errors.MappingError;

            await _dbContext.Teams.AddRangeAsync(teams, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return ErrorOr.ErrorOr.From(teams);
        }
    }
}
