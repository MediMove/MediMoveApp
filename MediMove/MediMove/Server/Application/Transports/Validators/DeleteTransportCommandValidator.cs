using FluentValidation;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;

namespace MediMove.Server.Application.Transports.Validators
{
    /// <summary>
    /// Validator for DeleteTransportsCommand.
    /// </summary>
    public class DeleteTransportCommandValidator : AbstractValidator<DeleteTransportsCommand>
    {
        /// <summary>
        /// Constructor for DeleteTransportsCommandValidator.
        /// </summary>
        /// <param name="dbContext">MediMoveDbContext</param>
        public DeleteTransportCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(x => x.Request)
                .NotEmpty().WithMessage("Request cannot be empty");

            RuleFor(x => x.Request.TransportIds)
                .NotEmpty().WithMessage("TransportIds cannot be empty")
                .CustomAsync(async (transportIds, context, cancellationToken) =>
                {
                    if (!dbContext.Transports
                        .Where(t => transportIds.Contains(t.Id))
                        .Count().Equals(transportIds.Length))
                        context.AddFailure("TransportIds", "One or more transport ids do not exist");
                    
                }).When(x => x.Request.TransportIds.Any());
        }
    }
}