using FluentValidation;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Teams.Validators
{
    /// <summary>
    /// Validator for DeleteTeamsCommand.
    /// </summary>
    public class DeleteTeamsCommandValidator : AbstractValidator<DeleteTeamsCommand>
    {
        /// <summary>
        /// Constructor for DeleteTeamsCommandValidator.
        /// </summary>
        /// <param name="dbContext">MediMoveDbContext</param>
        public DeleteTeamsCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(x => x.Request)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(x => x.Request.TeamIds)
                .CustomAsync(async (TeamIds, context, cancellationToken) =>
                {
                    var teams = await dbContext.Teams.Where(t => TeamIds.Contains(t.Id))
                        .Select(t => new { t.Day.Date, t.ShiftType })
                        .Distinct()
                        .ToArrayAsync(cancellationToken);

                    if (teams.Length != TeamIds.Count)
                        context.AddFailure("Request.TeamIds", "One or more teams are invalid");

                    if (teams.Any(t => !TeamValidatiors.CanExecuteCommands(t.Date, t.ShiftType)))
                        context.AddFailure("Request", "One or more teams cannot be deleted");
                })
                .When(command => command.Request != null
                    && command.Request.TeamIds != null
                    && command.Request.TeamIds.Any())
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}