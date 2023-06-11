using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Handlers
{
    /// <summary>
    /// Handler for cancelling transports.
    /// </summary>
    public class CancelTransportsCommandHandler : IRequestHandler<CancelTransportsCommand, ErrorOr<Unit>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for CancelTransportsCommandHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public CancelTransportsCommandHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the CancelTransportsCommand.
        /// </summary>
        /// <param name="command">CancelTransportsCommand</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Unit wrapped in ErrorOr</returns>
        public async Task<ErrorOr<Unit>> Handle(CancelTransportsCommand command, CancellationToken cancellationToken)
        {
            await _dbContext.Transports
                .Where(t => command.Request.TransportIds.Contains(t.Id))
                .ForEachAsync(t => t.IsCancelled = true, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ErrorOr<Unit>();
        }
    }
}