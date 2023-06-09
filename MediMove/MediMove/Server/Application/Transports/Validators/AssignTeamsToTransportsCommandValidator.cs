using FluentValidation;
using MediMove.Server.Application.Transports.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;
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
            RuleFor(x => x.Request).NotEmpty().WithMessage("Request cannot be empty");
            RuleFor(x => x.Request.TransportsToTeams)
                .NotNull().WithMessage("TransportsToTeams cannot be null")
                .CustomAsync(async (transportsToTeams, context, cancellationToken) =>
                {
                    if (!transportsToTeams.Keys.Any())
                    {
                        context.AddFailure("Request.TransportsToTeams", "TransportsToTeams cannot be empty");
                        return;
                    }

                    var transports = await dbContext.Transports
                        .Where(t => transportsToTeams.Keys.Contains(t.Id))
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

                    var teamsOk = dbContext.Teams
                        .Where(t => transportsToTeams.Values.Contains(t.Id) &&
                            t.Day.Date == datesAndShifts[0].Day)
                        .Include(t => t.Driver)
                        .ThenInclude(p => p.Availabilities)
                        .Where(t => t.Driver.Availabilities.Any(a =>
                            a.Day.Date == datesAndShifts[0].Day &&
                            a.ShiftType == datesAndShifts[0].ShiftType))
                        .Count().Equals(transportsToTeams.Values.Count);

                    if (!teamsOk)
                        context.AddFailure("Request.TransportsToTeams", "All teams must be valid");

                }).Unless(x => x.Request == null ||
                    x.Request.TransportsToTeams == null);
        }
    }
}