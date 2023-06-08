using FluentValidation;
using MediMove.Server.Application.Availabilities.Queries;

namespace MediMove.Server.Application.Availabilities.Validators
{
    /// <summary>
    /// Validator for GetAvailableParamedicsByDayAndShiftQuery.
    /// </summary>
    public class GetAvailableParamedicsByDayAndShiftValidator : AbstractValidator<GetAvailableParamedicsByDayAndShiftQuery>
    {
        /// <summary>
        /// Constructor for GetAvailableParamedicsByDayAndShiftValidator.
        /// </summary>
        public GetAvailableParamedicsByDayAndShiftValidator()
        {
            RuleFor(x => x.Shift).IsInEnum().WithMessage("Shift must be a valid ShiftType");
            RuleFor(x => x.Day).NotEmpty().WithMessage("Date cannot be empty");
            RuleFor(x => x.Day.TimeOfDay).Empty().WithMessage("Date cannot have a time component");
        }
    }
}