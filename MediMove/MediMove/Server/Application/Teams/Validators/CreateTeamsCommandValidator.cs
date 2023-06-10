using FluentValidation;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Teams.Validators
{
    /// <summary>
    /// Validator for CreateTeamsCommand.
    /// </summary>
    public class CreateTeamsCommandValidator : AbstractValidator<CreateTeamsCommand>
    {
        /// <summary>
        /// Constructor for CreateTeamsCommandValidator.
        /// </summary>
        /// <param name="dbContext">MediMoveDbContext</param>
        public CreateTeamsCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(x => x.Request.Day)
                .NotEmpty().WithMessage("Day cannot be empty.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Day cannot be in the past.");

            RuleFor(x => x.Request.Shift)
                .IsInEnum().WithMessage("Shift must be a valid ShiftType");

            RuleFor(x => x.Request)
                .NotEmpty().WithMessage("Request cannot be empty.")
                .Must(r => r.Teams.Any()).WithMessage("Teams list cannot be empty.")
                .CustomAsync(async (request, context, cancellationToken) =>
                {
                    var ids = request.Teams.SelectMany(t => new[] { t.DriverId, t.ParamedicId }).ToHashSet();

                    if (ids.Count != request.Teams.Count() * 2)
                    {
                        context.AddFailure("request.Teams", "All IDs must be distinct.");
                        return;
                    }
                    if (await dbContext.Teams
                        .AnyAsync(t => t.Day.Date == request.Day.Date &&
                            t.ShiftType == request.Shift &&
                        (ids.Contains(t.DriverId) || ids.Contains(t.ParamedicId)),
                        cancellationToken))
                    {
                        context.AddFailure("request.Teams",
                                    "At least one paramedic is already assigned to team for this day.");
                        return;
                    }

                    var relatedParamedicsDict = await dbContext.Paramedics
                        .Where(p => ids.Contains(p.Id) && p.IsWorking)
                        .Include(p => p.Availabilities)
                        .SelectMany(p => p.Availabilities
                            .Where(a => a.Day.Date == request.Day.Date &&
                                (!a.ShiftType.HasValue || a.ShiftType == request.Shift)),
                            (p, a) => new { p.Id, p.IsDriver, a.ShiftType })
                        .ToDictionaryAsync(p => p.Id, cancellationToken);

                    if (relatedParamedicsDict.Count != ids.Count)
                    {
                        context.AddFailure("request.Teams",
                            "At least one paramedic is invaild or does not have availability for a given day.");
                        return;
                    }

                    if (request.Teams.Any(t => relatedParamedicsDict[t.DriverId].ShiftType.HasValue && relatedParamedicsDict[t.ParamedicId].ShiftType.HasValue &&
                        relatedParamedicsDict[t.DriverId].ShiftType != relatedParamedicsDict[t.ParamedicId].ShiftType))
                    {
                        context.AddFailure("request.Teams", "Paramedic and driver cannot have different shifts.");
                        return;
                    }

                    if (relatedParamedicsDict.Any(p => request.Teams
                        .Select(t => t.DriverId)
                        .ToHashSet()
                        .Contains(p.Value.Id) && !p.Value.IsDriver))
                            context.AddFailure("request.Teams", "At least one assigned driver is not a driver.");
                });
        }
    }
}