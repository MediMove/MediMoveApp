using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Models.Enums;
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
            RuleFor(x => x.Request)
                .NotNull().WithMessage("Request cannot be empty.");

            RuleFor(x => x.Request.Availabilities)
                .NotEmpty().WithMessage("Availabilities list cannot be empty.")
                .When(x => x != null && x.Request.Availabilities != null && x.Request.Availabilities.Any())
                .Must(declatations => declatations.Select(a => a.Day.Date).Distinct().Count() == declatations.Count()).WithMessage("Days must be unique")
                .Must(declatations => declatations.All(a => a.Day.Date >= DateTime.Today.Date)).WithMessage("Days must be in the future")
                .Must(declatations => declatations.All(a => !a.Shift.HasValue || Enum.IsDefined(typeof(ShiftType), a.Shift))).WithMessage("Shift must be null or a valid ShiftType");

            RuleFor(x => x)
                .Must(p => p.ParamedicId > 0).WithMessage("ParamedicId must be greater than 0")
                .When(p => p.ParamedicId > 0)
                .CustomAsync(async (x, context, cancellationToken) =>
                {
                    var paramedic = await dbContext.Paramedics
                        .Where(p => p.IsWorking)
                        .Include(p => p.Availabilities)
                        .Select(p => new
                        {
                            p.Id,
                            Availabilities = p.Availabilities.Select(a => a.Day.Date).ToHashSet()
                        })
                        .SingleOrDefaultAsync(p => p.Id == x.ParamedicId, cancellationToken);

                    if (paramedic is null)
                    {
                        context.AddFailure("ParamedicId", "Paramedic with provided id does not exist or is not working");
                        return;
                    }

                    if (x.Request.Availabilities.Any(a => paramedic.Availabilities.Contains(a.Day.Date)))
                        context.AddFailure("Availabilities", "Paramedic already has availability on one or more of the provided days");
                });
        }
    }
}
