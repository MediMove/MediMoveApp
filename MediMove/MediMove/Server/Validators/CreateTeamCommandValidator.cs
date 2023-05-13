using FluentValidation;
using MediMove.Server.Application.Teams.Commands;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Validators
{
    /// <summary>
    /// Validator for the CreateTeamDTO
    /// </summary>
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        /// <summary>
        /// Validates the CreateTeamDTO
        /// </summary>
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.dto.ParamedicId).GreaterThan(0);
            RuleFor(x => x.dto.DriverId).GreaterThan(0);
            RuleFor(x => x.dto.Day).Must(day => day > DateTime.Today);
        }
    }
}
