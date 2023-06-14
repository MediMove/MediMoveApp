using FluentValidation;
using MediMove.Server.Application.Availabilities.Queries;

namespace MediMove.Server.Application.Availabilities.Validators
{
    /// <summary>
    /// Validator for GetAvailableParamedicsByDateAndShiftQuery.
    /// </summary>
    public class GetAvailableParamedicsByDateAndShiftQueryValidator : AbstractValidator<GetAvailableParamedicsByDateAndShiftQuery>
    {
        /// <summary>
        /// Constructor for GetAvailableParamedicsByDateAndShiftQueryValidator.
        /// </summary>
        public GetAvailableParamedicsByDateAndShiftQueryValidator()
        {
            RuleFor(x => x.Shift).IsInEnum().WithMessage("Shift must be a valid ShiftType");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date cannot be empty");
        }
    }
}