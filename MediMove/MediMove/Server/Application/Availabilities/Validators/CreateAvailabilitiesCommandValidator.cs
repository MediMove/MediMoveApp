using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Models.Enums;
using MediMove.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Validators
{
    /// <summary>
    /// Validator for CreateAvailabilitiesCommand.
    /// </summary>
    public class CreateAvailabilitiesCommandValidator : AbstractValidator<CreateAvailabilitiesCommand>
    {
        /// <summary>
        /// Constructor for CreateAvailabilitiesCommandValidator.
        /// </summary>
        /// <param name="dbContext">MediMoveDbContext</param>
        public CreateAvailabilitiesCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(command => command.ParamedicId)
                .GreaterThan(0).WithMessage("{PropertyName} with value {PropertyValue} must be greater than 0");

            RuleFor(command => command.Request)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
            
            When(command => command.Request != null, () =>
            {
                RuleFor(command => command.Request.Availabilities)
                    .Must(declatations => declatations.All(a => AvailabilityValidatiors.CanParamedicModifyAvailability(a.Key.Date, a.Value))).WithMessage("At least one of the provided dates is invalid")
                    .Must(declatations => declatations.All(a => !a.Value.HasValue || Enum.IsDefined(typeof(ShiftType), a.Value))).WithMessage("Shift must be null or a valid ShiftType")
                    .When(command => command.Request.Availabilities != null)
                    .NotEmpty().WithMessage("{PropertyName} cannot be empty");
                
                RuleFor(command => command)
                    .CustomAsync(async (command, context, cancellationToken) =>
                    {
                        var paramedic = await dbContext.Paramedics
                            .Where(p => p.IsWorking)
                            .Include(p => p.Availabilities)
                            .Select(p => new
                            {
                                p.Id,
                                Availabilities = p.Availabilities.Select(a => a.Day.Date).ToHashSet()
                            })
                            .SingleOrDefaultAsync(p => p.Id == command.ParamedicId, cancellationToken);

                        if (paramedic is null)
                        {
                            context.AddFailure("ParamedicId", "Paramedic with provided ID does not exist or is not working");
                            return;
                        }

                        if (command.Request.Availabilities.Any(a => paramedic.Availabilities.Contains(a.Key.Date)))
                            context.AddFailure("Request.Availabilities", "Paramedic already has availability on one or more of the provided days");
                    })
                    .When(command => command.ParamedicId > 0 && command.Request.Availabilities != null && command.Request.Availabilities.Any());
            });
        }
    }
}