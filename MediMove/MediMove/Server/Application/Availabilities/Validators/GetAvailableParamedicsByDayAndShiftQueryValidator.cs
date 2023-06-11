using FluentValidation;
using MediMove.Server.Application.Availabilities.Queries;

namespace MediMove.Server.Application.Availabilities.Validators
{
    /// <summary>
    /// Validator for GetAvailableParamedicsByDayAndShiftQuery.
    /// </summary>
    public class GetAvailableParamedicsByDayAndShiftQueryValidator : AbstractValidator<GetAvailableParamedicsByDayAndShiftQuery>
    {
        /// <summary>
        /// Constructor for GetAvailableParamedicsByDayAndShiftQueryValidator.
        /// </summary>
        public GetAvailableParamedicsByDayAndShiftQueryValidator()
        {
            RuleFor(x => x.Shift).IsInEnum().WithMessage("Shift must be a valid ShiftType");
            RuleFor(x => x.Day).NotEmpty().WithMessage("Date cannot be empty");
        }
    }
}