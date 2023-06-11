using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Handlers
{
    /// <summary>
    /// Handler for deleting transports.
    /// </summary>
    public class DeleteTransportsCommandHandler : IRequestHandler<DeleteTransportsCommand, ErrorOr<Unit>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for DeleteTransportsCommandHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public DeleteTransportsCommandHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the DeleteTransportsCommand.
        /// </summary>
        /// <param name="command">DeleteTransportsCommand</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Unit wrapped in ErrorOr</returns>
        public async Task<ErrorOr<Unit>> Handle(DeleteTransportsCommand command, CancellationToken cancellationToken)
        {
            var transports = await _dbContext.Transports
                .Where(t => command.Request.TransportIds.Contains(t.Id))
                .ExecuteDeleteAsync(cancellationToken);

            return new ErrorOr<Unit>();
        }
    }
}