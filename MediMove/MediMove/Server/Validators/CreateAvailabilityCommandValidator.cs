using FluentValidation;
using MediMove.Server.Application.Availabilities.Commands;

namespace MediMove.Server.Validators
{
    public class CreateAvailabilityCommandValidator : AbstractValidator<CreateAvailabilitiesCommand>
    {
        public CreateAvailabilityCommandValidator()
        {
            RuleFor(x => x.Dto.Days).ForEach(r => r.Must(d => d.Date > DateTime.Today.Date));
            RuleFor(x => x.Dto.ShiftTypes).ForEach(r => r.IsInEnum());
            //Reguła dla paramedic id trzeba zrobić custom żeby sprawdzało czy paramedic istnieje, chociaż jak damy autoryzacje to chyba nie bedzie trzeba tego sprawdzac
        }
    }
}
