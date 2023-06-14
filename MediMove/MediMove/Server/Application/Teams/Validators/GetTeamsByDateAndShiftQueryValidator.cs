using FluentValidation;
using MediMove.Server.Application.Teams.Queries;

namespace MediMove.Server.Application.Teams.Validators
{
    /// <summary>
    /// Validator for GetTeamsByDateAndShiftQuery.
    /// </summary>
    public class GetTeamsByDateAndShiftQueryValidator : AbstractValidator<GetTeamsByDateAndShiftQuery>
    {
        /// <summary>
        /// Constructor for GetTeamsByDateAndShiftQueryValidator.
        /// </summary>
        public GetTeamsByDateAndShiftQueryValidator()
        {
            RuleFor(x => x.Shift).IsInEnum().WithMessage("{PropertyName} must be a valid ShiftType");
            RuleFor(x => x.Date).NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}