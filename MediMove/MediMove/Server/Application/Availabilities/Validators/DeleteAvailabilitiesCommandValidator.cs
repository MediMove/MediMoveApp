using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Validators
{
    /// <summary>
    /// Validator for DeleteAvailabilitiesCommand.
    /// </summary>
    public class DeleteAvailabilitiesCommandValidator : AbstractValidator<DeleteAvailabilitiesCommand>
    {
        /// <summary>
        /// Constructor for DeleteAvailabilitiesCommandValidator.
        /// </summary>
        /// <param name="dbContext">MediMoveDbContext</param>
        public DeleteAvailabilitiesCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(x => x.ParamedicId)
                .GreaterThan(0).WithMessage("ParamedicId must be greater than 0");

            RuleFor(x => x.Request)
                .NotEmpty().WithMessage("Request cannot be empty");

            RuleFor(x => x.Request.AvailabilityDates)
                .NotEmpty().WithMessage("AvailabilityDates array cannot be empty")
                .Must(a => a.All(day => day.Date >= DateTime.Today &&
                    day.Date < DateTime.Today.AddMonths(1)))
                .WithMessage("All dates must be in the future and within the next month");

            RuleFor(x => x)
                .CustomAsync(async (command, context, cancellationToken) =>
                {
                    var availabilityDatesNormalized = command.Request.AvailabilityDates.Select(d => d.Date);
                    if (!dbContext.Availabilities
                        .Where(a => a.ParamedicId == command.ParamedicId && availabilityDatesNormalized.Contains(a.Day.Date))
                        .Count().Equals(command.Request.AvailabilityDates.Length))
                    {
                        context.AddFailure("Request.AvailabilityDates", "Paramedic does not have availability on one or more of the provided days");
                        return;
                    }

                    if (await dbContext.Teams
                        .AnyAsync(t => (t.DriverId == command.ParamedicId || t.ParamedicId == command.ParamedicId) &&
                            availabilityDatesNormalized.Contains(t.Day.Date), cancellationToken))
                        context.AddFailure("Request.AvailabilityDates", "Paramedic has a team on one or more of the provided days");
                }).When(x =>
                    x.ParamedicId > 0 &&
                    x.Request.AvailabilityDates.Any()
                );
        }
    }
}