using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.Enums;
using MediMove.Shared.Validators;
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
            RuleFor(command => command.ParamedicId)
                .GreaterThan(0).WithMessage("{PropertyName} with value {PropertyValue} must be greater than 0");

            RuleFor(command => command.Request)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");

            When(command => command.Request != null, () =>
            {
                RuleFor(command => command.Request.AvailabilityDates)
                    .Must(AvailabilityDates => AvailabilityDates.All(date => AvailabilityValidatiors.IsValidFutureDate(date) || date.Date == DateTime.Today)).WithMessage("At least one of the provided dates in {PropertyName} is invalid")
                    .When(command => command.Request.AvailabilityDates != null)
                    .NotEmpty().WithMessage("{PropertyName} cannot be empty");

                RuleFor(command => command)
                    .CustomAsync(async (command, context, cancellationToken) =>
                    {
                        var availabilityDatesNormalized = command.Request.AvailabilityDates.Select(d => d.Date);
                        var availabilities = await dbContext.Availabilities
                            .Where(a => a.ParamedicId == command.ParamedicId && availabilityDatesNormalized.Contains(a.Day.Date))
                            .Select(a => new { a.Day.Date, ShiftType = a.ShiftType ?? ShiftType.Morning })
                            .ToArrayAsync(cancellationToken);

                        if (availabilities.Length != command.Request.AvailabilityDates.Count)
                        {
                            context.AddFailure("Request.AvailabilityDates", "Paramedic does not have availability on one or more of the provided dates");
                            return;
                        }

                        if (availabilities.Any(a => a.Date == DateTime.Today && !AvailabilityValidatiors.IsBeforeShift(a.ShiftType)))
                        {
                            context.AddFailure("Request.AvailabilityDates", "Shift has already started today");
                            return;
                        }

                        if (await dbContext.Teams
                            .AnyAsync(t => (t.DriverId == command.ParamedicId || t.ParamedicId == command.ParamedicId) &&
                                availabilityDatesNormalized.Contains(t.Day.Date), cancellationToken))
                            context.AddFailure("Request.AvailabilityDates", "Paramedic has a team on one or more of the provided dates");
                    })
                    .When(command =>
                        command.ParamedicId > 0 &&
                        command.Request.AvailabilityDates != null &&
                        command.Request.AvailabilityDates.Any());
            });
        }
    }
}