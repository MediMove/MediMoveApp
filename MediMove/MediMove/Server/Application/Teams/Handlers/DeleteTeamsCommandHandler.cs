using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Teams.Handlers
{
    /// <summary>
    /// Handler for deleting teams.
    /// </summary>
    public class DeleteTeamsCommandHandler : IRequestHandler<DeleteTeamsCommand, ErrorOr<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for DeleteTeamsCommandHandler.
        /// </summary>
        /// <param name="mapper">mapper to inject</param>
        /// <param name="dbContext">dbContext to inject</param>
        public DeleteTeamsCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the DeleteTeamsCommand.
        /// </summary>
        /// <param name="command">DeleteTeamsCommand</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Unit wrapped in ErrorOr</returns>
        public async Task<ErrorOr<Unit>> Handle(DeleteTeamsCommand command, CancellationToken cancellationToken)
        {
            await _dbContext.Teams
                .Where(t => command.Request.TeamIds.Contains(t.Id))
                .ExecuteDeleteAsync(cancellationToken);

            return new ErrorOr<Unit>();
        }
    }
}