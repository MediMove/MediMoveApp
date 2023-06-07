using FluentValidation;
using MediMove.Server.Application.Availabilities.Queries;

namespace MediMove.Server.Validators
{
    public class GetAvailableParamedicsByDayAndShiftValidator : AbstractValidator<GetAvailableParamedicsByDayAndShiftQuery>
    {
        public GetAvailableParamedicsByDayAndShiftValidator()
        {
            RuleFor(x => x.Day.Date).GreaterThanOrEqualTo(DateTime.Today.Date);
            RuleFor(x => x.Shift).IsInEnum();
        }
    }
}