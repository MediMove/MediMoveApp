using FluentValidation;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;
using MediMove.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Validators
{
    /// <summary>
    /// Validator for AssignTeamsToTransportsCommand.
    /// </summary>
    public class AssignTeamsToTransportsCommandValidator : AbstractValidator<AssignTeamsToTransportsCommand>
    {
        /// <summary>
        /// Constructor for AssignTeamsToTransportsCommandValidator.
        /// </summary>
        /// <param name="dbContext">MediMoveDbContext</param>
        public AssignTeamsToTransportsCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(command => command.Request).NotEmpty().WithMessage("{PropertyName} cannot be empty");

            RuleFor(command => command.Request.TransportsToTeams)
                .CustomAsync(async (transportsToTeams, context, cancellationToken) =>
                {
                    if (!transportsToTeams.Keys.Any())
                    {
                        context.AddFailure("Request.TransportsToTeams", "TransportsToTeams cannot be empty");
                        return;
                    }


                    var transports = await dbContext.Transports
                        .Where(t => !t.IsCancelled && transportsToTeams.Keys.Contains(t.Id))
                        .Select(t => t.StartTime)
                        .ToArrayAsync(cancellationToken);

                    if (transports.Length != transportsToTeams.Keys.Count)
                    {
                        context.AddFailure("Request.TransportsToTeams", "All transports must exist");
                        return;
                    }
                    var datesAndShifts = transports.Select(startTime => new { Day = startTime.Date, ShiftType = startTime.ToShiftType().Value })
                        .Distinct().ToArray();

                    if (datesAndShifts.Length != 1)
                    {
                        context.AddFailure("Request.TransportsToTeams", "All transports must have the same date and shiftType");
                        return;
                    }

                    if (!TransportValidator.CanAssignTeam(datesAndShifts[0].Day))
                    {
                        context.AddFailure("Request.TransportsToTeams", "Cannot assign teams to this transport due to its date");
                        return;
                    }

                    var teamsOk = dbContext.Teams
                        .Count(t => transportsToTeams.Values.Contains(t.Id) &&
                                    t.Day.Date == datesAndShifts[0].Day &&
                                    t.ShiftType == datesAndShifts[0].ShiftType)
                        .Equals(transportsToTeams.Values.Distinct().Count());

                    if (!teamsOk)
                        context.AddFailure("Request.TransportsToTeams", "All teams must exist and have the same date and shiftType");

                })
                .When(command => command.Request != null &&
                    command.Request.TransportsToTeams != null)
                .NotNull().WithMessage("{PropertyName} cannot be null");
        }
    }
}