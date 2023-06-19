using FluentValidation;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Validators
{
    /// <summary>
    /// Validator for CancelTransportsCommand.
    /// </summary>
    public class CancelTransportsCommandValidator : AbstractValidator<CancelTransportsCommand>
    {
        /// <summary>
        /// Constructor for CancelTransportsCommandValidator.
        /// </summary>
        /// <param name="dbContext">MediMoveDbContext</param>
        public CancelTransportsCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(x => x.Request)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.Request.TransportIds)
                .CustomAsync(async (transportIds, context, cancellationToken) =>
                {
                    var transportDates = await dbContext.Transports
                        .Where(t => transportIds.Contains(t.Id) && !t.IsCancelled)
                        .Select(t => t.StartTime)
                        .ToArrayAsync(cancellationToken);

                    if (transportDates.Length != transportIds.Count)
                        context.AddFailure("Request.TransportIds", "One or more transport IDs are invalid");

                    if (transportDates.Any(t => !TransportValidator.CanCancelTransport(t)))
                        context.AddFailure("Request.TransportIds", "One or more transports cannot be cancelled");
                })
                .When(x => x.Request != null &&
                    x.Request.TransportIds != null &&
                    x.Request.TransportIds.Any())
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}