﻿using FluentValidation;
using MediMove.Server.Application.Availabilities.Queries;

namespace MediMove.Server.Application.Availabilities.Validators
{
    /// <summary>
    /// Validator for GetAvailabilitiesForParamedicByDateRangeQuery.
    /// </summary>
    public class GetAvailabilitiesForParamedicByDateRangeQueryValidator : AbstractValidator<GetAvailabilitiesForParamedicByDateRangeQuery>
    {
        /// <summary>
        /// Constructor for GetAvailabilitiesForParamedicByDateRangeQueryValidator.
        /// </summary>
        public GetAvailabilitiesForParamedicByDateRangeQueryValidator()
        {
            RuleFor(x => x.ParamedicId)
                .GreaterThan(0).WithMessage("ParamedicId must be greater than 0");

            RuleFor(x => x.StartDateInclusive)
                .LessThanOrEqualTo(x => x.EndDateInclusive).WithMessage("StartDateInclusive must be less than or equal to EndDateInclusive");
        }
    }
}