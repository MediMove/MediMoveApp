using FluentValidation;
using MediMove.Server.Application.Patients.Commands;

namespace MediMove.Server.Application.Patients.Validators
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.Dto.FirstName).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 25);

            RuleFor(x => x.Dto.LastName).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 25);

            RuleFor(x => x.Dto.StreetAddress).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 30);

            RuleFor(x => x.Dto.HouseNumber).NotEmpty()
                .Matches(@"^[a-zA-Z0-9\s-]+$").Length(1, 10);

            RuleFor(x => x.Dto.ApartmentNumber).LessThanOrEqualTo(200);

            RuleFor(x => x.Dto.PostalCode).NotEmpty()
                .Matches(@"^\d{2}-\d{3}(\d{2})?$");

            RuleFor(x => x.Dto.StateProvince).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 30);

            RuleFor(x => x.Dto.City).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 30);

            RuleFor(x => x.Dto.Country).NotEmpty()
                .Matches(@"^[a-zA-Z\s-]+$").Length(2, 30);

            RuleFor(x => x.Dto.PhoneNumber).NotEmpty()
                .Matches(@"^[0-9]+([- ]?[0-9]+)*$").Length(2, 30);

            RuleFor(x => x.Dto.Weight).NotEmpty();
        }
    }
}
