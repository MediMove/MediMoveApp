using FluentValidation;
using MediMove.Server.Application.Authentication.Commands;
using MediMove.Server.Application.Shared;
using MediMove.Shared.Validators;

namespace MediMove.Server.Application.Authentication.Validators
{
    /// <summary>
    /// Validator for <see cref="RegisterParamedicCommand"/>.
    /// </summary>
    public class RegisterParamedicCommandValidator : AbstractValidator<RegisterParamedicCommand>
    {
        /// <summary>
        /// Constructor for <see cref="RegisterParamedicCommandValidator"/>.
        /// </summary>
        public RegisterParamedicCommandValidator()
        {
            RuleFor(x => x.Request.FirstName).FirstName();

            RuleFor(x => x.Request.LastName).LastName();

            RuleFor(x => x.Request.StreetAddress).StreetAddress();

            RuleFor(x => x.Request.HouseNumber).HouseNumber();

            RuleFor(x => x.Request.ApartmentNumber).ApartmentNumber();

            RuleFor(x => x.Request.PostalCode).PostalCode();

            RuleFor(x => x.Request.StateProvince).StateProvince();

            RuleFor(x => x.Request.City).City();

            RuleFor(x => x.Request.Country).Country();

            RuleFor(x => x.Request.PhoneNumber).PhoneNumber();

            RuleFor(x => x.Request.BankAccountNumber).BankAccountNumber();

            RuleFor(x => x.Request.PayPerHour).Must(s => s.IsValidPayPerHour()).WithMessage("Invalid {PropertyName}");
        }
    }
}