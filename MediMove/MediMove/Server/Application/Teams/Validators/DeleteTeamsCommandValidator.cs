using FluentValidation;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Server.Data;
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
                .NotEmpty().WithMessage("Request cannot be empty.");

            RuleFor(x => x.Request.TeamIds)
                .NotEmpty().WithMessage("TeamIds cannot be empty.")
                .CustomAsync(async (TeamIds, context, cancellationToken) =>
                {
                    if (!dbContext.Teams
                        .Where(t => TeamIds.Contains(t.Id))
                        .Count().Equals(TeamIds.Length))
                        context.AddFailure("TeamIds", "One or more teams do not exist");
                }).Unless(x => x.Request == null || x.Request.TeamIds == null || !x.Request.TeamIds.Any());
        }
    }
}