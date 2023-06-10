using FluentValidation;
using MediMove.Server.Application.Availabilities.Queries;

namespace MediMove.Server.Application.Availabilities.Validators
{
    /// <summary>
    /// Validator for GetAvailabilitiesByParamedicQuery.
    /// </summary>
    public class GetAvailabilitiesByParamedicQueryValidator : AbstractValidator<GetAvailabilitiesByParamedicQuery>
    {
        /// <summary>
        /// Constructor for GetAvailabilitiesByParamedicQueryValidator.
        /// </summary>
        public GetAvailabilitiesByParamedicQueryValidator()
        {
            RuleFor(x => x.ParamedicId)
                .GreaterThan(0).WithMessage("ParamedicId must be greater than 0");
        }
    }
}