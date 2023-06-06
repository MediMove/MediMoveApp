using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Validators
{
    public class CreateAvailabilityCommandValidator : AbstractValidator<CreateAvailabilitiesCommand>
    {
        public CreateAvailabilityCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(x => x.Dto.Availabilities)
                .NotEmpty().WithMessage("Availabilities must be provided")
                .Must(x => x.Distinct().Count() == x.Count()).WithMessage("Availabilities must be unique")
                .ForEach(e => e.Must(a => a.Day > DateTime.Today.AddDays(1).Date).WithMessage("Day must be in the future")
                .Must(a => Enum.IsDefined(typeof(ShiftType), a.Shift)).WithMessage("Day must be in the future"));

            RuleFor(x => x.ParamedicId)
                .NotEmpty().WithMessage("ParamedicId must be provided")
                .GreaterThan(0);

            RuleFor(x => x)
                .CustomAsync(async (x, context, cancellationToken) =>
                {
                    // Get paramedic by id
                    var paramedic = await dbContext.Paramedics
                        .Include(p => p.Availabilities)
                        .FirstOrDefaultAsync(p => p.Id == x.ParamedicId, cancellationToken: cancellationToken);

                    if (paramedic is null)
                    {
                        context.AddFailure("ParamedicId", "Paramedic does not exist");
                        return;
                    }

                    // Create a HashSet to store the dates of paramedic's availabilities
                    var paramedicAvailabilityDates = new HashSet<DateTime>(paramedic.Availabilities.Select(a => a.Day.Date));

                    // Check if any of the new availabilities conflict with the paramedic's existing availabilities
                    foreach (var (Day, Shift) in x.Dto.Availabilities)
                    {
                        if (paramedicAvailabilityDates.Contains(Day.Date))
                        {
                            context.AddFailure("Availabilities", $"Paramedic already has availability on {Day}");
                            return;
                        }
                    }
                });
        }
    }
}
