using FluentValidation;
using MediMove.Server.Application.Transports.Queries;

namespace MediMove.Server.Application.Transports.Validators
{
    /// <summary>
    /// Validator for GetTransportsByDayAndShiftQuery.
    /// </summary>
    public class GetTransportsByDayAndShiftValidator : AbstractValidator<GetTransportsByDayAndShiftQuery>
    {
        /// <summary>
        /// Constructor for GetTransportsByDayAndShiftValidator.
        /// </summary>
        public GetTransportsByDayAndShiftValidator()
        {
            RuleFor(x => x.Day)
                .NotEmpty().WithMessage("Day cannot be empty");

            RuleFor(x => x.Shift)
                .IsInEnum().WithMessage("Shift must be a valid ShiftType");
        }
    }
}