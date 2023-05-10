using FluentValidation;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Validators
{
    /// <summary>
    /// Validator for the CreateTeamDTO
    /// </summary>
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamDTO>
    {
        /// <summary>
        /// Validates the CreateTeamDTO
        /// </summary>
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.ParamedicId).GreaterThan(0);
            RuleFor(x => x.DriverId).GreaterThan(0);
            RuleFor(x => x.Day).Must(day => day > DateTime.Today.ToDateOnly());
        }
    }
}
