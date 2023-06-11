using FluentValidation;
using MediMove.Server.Application.Transports.Queries;

namespace MediMove.Server.Application.Transports.Validators
{
    /// <summary>
    /// Validator for GetTransportsByParamedicAndDateRangeQuery.
    /// </summary>
    public class GetTransportsByParamedicAndDateRangeQueryValidator : AbstractValidator<GetTransportsByParamedicAndDateRangeQuery>
    {
        /// <summary>
        /// Constructor for GetTransportsByParamedicAndDateRangeQueryValidator.
        /// </summary>
        public GetTransportsByParamedicAndDateRangeQueryValidator()
        {
            RuleFor(x => x.ParamedicId)
                .GreaterThan(0).WithMessage("ParamedicId must be greater than 0");

            RuleFor(x => x.StartDateInclusive)
                .LessThanOrEqualTo(x => x.EndDateInclusive).WithMessage("StartDateInclusive must be less than or equal to EndDateInclusive");
        }
    }
}