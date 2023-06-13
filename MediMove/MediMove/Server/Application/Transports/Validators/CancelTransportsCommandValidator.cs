using FluentValidation;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;

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
                .NotEmpty().WithMessage("Request cannot be empty");

            RuleFor(x => x.Request.TransportIds)
                .NotEmpty().WithMessage("TransportIds cannot be empty")
                .CustomAsync(async (transportIds, context, cancellationToken) =>
                {
                    if (!dbContext.Transports
                        .Where(t => transportIds.Contains(t.Id))
                        .Count().Equals(transportIds.Count))
                        context.AddFailure("Request.TransportIds", "One or more transport ids do not exist");
                })
                .When(x => x.Request != null &&
                    x.Request.TransportIds != null &&
                    x.Request.TransportIds.Any());
        }
    }
}