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
        /// <param name="_dbContext">MediMoveDbContext</param>
        public CreateTeamsCommandValidator(MediMoveDbContext _dbContext)
        {
            RuleFor(x => x.Request.Day);
                //.NotEmpty().WithMessage("Day cannot be empty.")
                //.GreaterThanOrEqualTo(DateTime.Today).WithMessage("Day cannot be in the past.");

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

                    if (await _dbContext.Teams
                        .AnyAsync(t => t.Day.Date == request.Day.Date &&
                        (ids.Contains(t.DriverId) || ids.Contains(t.ParamedicId)),
                        cancellationToken))
                    {
                        context.AddFailure("request.Teams",
                                "At least one paramedic is already assigned to team for this day.");
                        return;
                    }
                            

                    var paramedics = await _dbContext.Paramedics
                        .Where(p => ids.Contains(p.Id))
                        .Include(p => p.Availabilities)
                        .SelectMany(p => p.Availabilities
                            .Where(a => a.Day.Date == request.Day.Date),
                            (p, a) => new { p.Id, p.IsWorking, p.IsDriver, a.ShiftType })
                        .ToDictionaryAsync(p => p.Id, cancellationToken);

                    if (paramedics.Count != ids.Count)
                    {
                        context.AddFailure("request.Teams",
                            "At least one paramedic does not exist or does not have availability for a given day.");
                        return;
                    }

                    if (paramedics.Any(p => !p.Value.IsWorking))
                    {
                        context.AddFailure("request.Teams", "At least one paramedic is not working.");
                        return;
                    }

                    if (!request.Teams.All(t => paramedics[t.DriverId].ShiftType == paramedics[t.ParamedicId].ShiftType))
                    {
                        context.AddFailure("request.Teams", "Paramedic and driver must have the same shift type.");
                        return;
                    }

                    if (paramedics.Any(p => request.Teams
                        .Select(t => t.DriverId)
                        .ToHashSet()
                        .Contains(p.Value.Id) && !p.Value.IsDriver))
                            context.AddFailure("request.Teams", "At least one assigned driver is not a driver.");
                });
        }
    }
}