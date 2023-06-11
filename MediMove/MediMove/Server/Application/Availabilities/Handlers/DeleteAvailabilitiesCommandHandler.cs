using ErrorOr;
using MediatR;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Handlers
{
    /// <summary>
    /// Handler for deleting availabilities.
    /// </summary>
    public class DeleteAvailabilitiesCommandHandler : IRequestHandler<DeleteAvailabilitiesCommand, ErrorOr<Unit>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for DeleteAvailabilitiesCommandHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public DeleteAvailabilitiesCommandHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the DeleteAvailabilitiesCommand.
        /// </summary>
        /// <param name="command">DeleteAvailabilitiesCommand</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Unit wrapped in ErrorOr</returns>
        public async Task<ErrorOr<Unit>> Handle(DeleteAvailabilitiesCommand command, CancellationToken cancellationToken)
        {
            await _dbContext.Availabilities
                .Where(t => t.ParamedicId == command.ParamedicId &&
                    command.Request.AvailabilityDates.Select(d => d.Date).Contains(t.Day.Date))
                .ExecuteDeleteAsync(cancellationToken);

            return new ErrorOr<Unit>();
        }
    }
}