using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
                .Must(a => a.All(day => day >= DateTime.Today))
                .WithMessage("Dates cannot be in the past");

            RuleFor(x => x)
                .CustomAsync(async (command, context, cancellationToken) =>
                {
                    var availabilityDatesNormalized = command.Request.AvailabilityDates.Select(d => d.Date);
                    var availabilities = await dbContext.Availabilities
                        .Where(a => a.ParamedicId == command.ParamedicId && availabilityDatesNormalized.Contains(a.Day.Date))
                        .Select(a => new { a.Day.Date, ShiftType = a.ShiftType == null ? ShiftType.Morning : a.ShiftType })
                        .ToArrayAsync(cancellationToken);

                    if (availabilities.Length != command.Request.AvailabilityDates.Length)
                    {
                        context.AddFailure("Request.AvailabilityDates", "Paramedic does not have availability on one or more of the provided days");
                        return;
                    }

                    if (availabilities.Any(a => a.Date == DateTime.Today && a.ShiftType.Value.StartTime() <= DateTime.Now.TimeOfDay))
                    {
                        context.AddFailure("Request.AvailabilityDates", "Shift has already started today");
                        return;
                    }

                    if (await dbContext.Teams
                        .AnyAsync(t => (t.DriverId == command.ParamedicId || t.ParamedicId == command.ParamedicId) &&
                            availabilityDatesNormalized.Contains(t.Day.Date), cancellationToken))
                        context.AddFailure("Request.AvailabilityDates", "Paramedic has a team on one or more of the provided days");
                })
                .When(x =>
                    x.ParamedicId > 0 &&
                    x.Request != null &&
                    x.Request.AvailabilityDates != null &&
                    x.Request.AvailabilityDates.Any());
        }
    }
}