using FluentValidation;
using MediMove.Server.Application.Availabilities.Queries;

namespace MediMove.Server.Application.Availabilities.Validators
{
    /// <summary>
    /// Validator for GetAvailabilitiesByParamedicAndDateRangeQuery.
    /// </summary>
    public class GetAvailabilitiesByParamedicAndDateRangeQueryValidator : AbstractValidator<GetAvailabilitiesByParamedicAndDateRangeQuery>
    {
        /// <summary>
        /// Constructor for GetAvailabilitiesByParamedicAndDateRangeQueryValidator.
        /// </summary>
        public GetAvailabilitiesByParamedicAndDateRangeQueryValidator()
        {
            RuleFor(query => query.ParamedicId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(query => query.StartDateInclusive)
                .LessThanOrEqualTo(x => x.EndDateInclusive).WithMessage("{PropertyName} must be less than or equal to EndDateInclusive");
        }
    }
}