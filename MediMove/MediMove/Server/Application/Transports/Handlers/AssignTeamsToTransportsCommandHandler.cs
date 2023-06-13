using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Handlers
{
    /// <summary>
    /// Handler for assigning teams to transports.
    /// </summary>
    public class AssignTeamsToTransportsCommandHandler : IRequestHandler<AssignTeamsToTransportsCommand, ErrorOr<Transport[]>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for AssignTeamsToTransportsCommandHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public AssignTeamsToTransportsCommandHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the AssignTeamsToTransportsCommand.
        /// </summary>
        /// <param name="command">AssignTeamsToTransportsCommand</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>array of Transports wrapped in ErrorOr</returns>
        public async Task<ErrorOr<Transport[]>> Handle(AssignTeamsToTransportsCommand command, CancellationToken cancellationToken)
        {
            var transports = await _dbContext.Transports
                .Where(t => command.Request.TransportsToTeams.Keys.Contains(t.Id))
                .OrderBy(t => t.Id)
                .ToArrayAsync(cancellationToken);

            var i = 0;
            foreach (var entry in command.Request.TransportsToTeams)
            {
                transports[i].TeamId = entry.Value;
                i++;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return transports;
        }
    }
}
