using FluentValidation;
using MediMove.Server.Application.Patients.Commands;

namespace MediMove.Server.Validators
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.Dto.FirstName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Dto.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Dto.StreetAddress)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Dto.HouseNumber)
                .NotEmpty()
                .MaximumLength(7);

            RuleFor(x => x.Dto.ApartmentNumber)
                .NotEmpty();

            RuleFor(x => x.Dto.PostalCode)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.Dto.City)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Dto.StateProvince)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Dto.Country)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Dto.Weight)
                .NotEmpty();
        }
    }
}
