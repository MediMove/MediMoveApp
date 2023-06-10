using FluentValidation;
using MediMove.Server.Application.Transports.Queries;

namespace MediMove.Server.Application.Transports.Validators
{
    /// <summary>
    /// Validator for GetTransportsByDayAndShiftQuery.
    /// </summary>
    public class GetTransportsByDayAndShiftQueryValidator : AbstractValidator<GetTransportsByDayAndShiftQuery>
    {
        /// <summary>
        /// Constructor for GetTransportsByDayAndShiftQueryValidator.
        /// </summary>
        public GetTransportsByDayAndShiftQueryValidator()
        {
            RuleFor(x => x.Day)
                .NotEmpty().WithMessage("Day cannot be empty");

            RuleFor(x => x.Shift)
                .IsInEnum().WithMessage("Shift must be a valid ShiftType");
        }
    }
}