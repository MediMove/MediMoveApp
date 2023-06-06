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
                .NotEmpty().WithMessage("Availabilities list cannot be empty.")
                .Must(a => a.Select(avail => avail.Day.Date).Distinct().Count() == a.Count()).WithMessage("Days must be unique")
                .Must(a => a.All(avail => avail.Day.Date >= DateTime.Today.AddDays(1).Date)).WithMessage("Days must be in the future")
                .Must(a => a.All(avail => Enum.IsDefined(typeof(ShiftType), avail.Shift))).WithMessage("Shift must be defined");

            RuleFor(x => x.ParamedicId)
               .NotEmpty().WithMessage("ParamedicId must be provided")
               .GreaterThan(0);

            RuleFor(x => x)
                .CustomAsync(async (x, context, cancellationToken) =>
                {
                    var paramedic = await dbContext.Paramedics
                        .Include(p => p.Availabilities)
                        .FirstOrDefaultAsync(p => p.Id == x.ParamedicId, cancellationToken);

                    if (paramedic is null)
                    {
                        context.AddFailure("ParamedicId", "Paramedic does not exist");
                        return;
                    }

                    // Create a HashSet to store the dates of paramedic's availabilities
                    var paramedicAvailabilityDates = new HashSet<DateTime>(paramedic.Availabilities.Select(a => a.Day.Date));

                    if (x.Dto.Availabilities.Any(a => paramedicAvailabilityDates.Contains(a.Day.Date)))
                        context.AddFailure("Availabilities", "Paramedic already has availability on one or more of the provided days");
                });
        }
    }
}
