using FluentValidation;
using MediMove.Server.Application.Employees.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Validators;

namespace MediMove.Server.Application.Employees.Validators
{
    /// <summary>
    /// Validator for <see cref="PutParamedicsCommand"/>.
    /// </summary>
    public class PutParamedicsCommandValidator : AbstractValidator<PutParamedicsCommand>
    {
        /// <summary>
        /// Constructor for <see cref="PutParamedicsCommandValidator"/>.
        /// </summary>
        /// <param name="dbContext"><see cref="MediMoveDbContext"/></param>
        public PutParamedicsCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(command => command.Paramedics)
                .Must(paramedic => paramedic.All(d =>
                    d.Id > 0 &&
                    d.Email.IsValidEmail() &&
                    d.FirstName.IsValidFirstName() &&
                    d.LastName.IsValidLastName() &&
                    d.StreetAddress.IsValidStreetAddress() &&
                    d.HouseNumber.IsValidHouseNumber() &&
                    d.ApartmentNumber.IsValidApartmentNumber() &&
                    d.City.IsValidCity() &&
                    d.PostalCode.IsValidPostalCode() &&
                    d.StateProvince.IsValidStateProvince() &&
                    d.Country.IsValidCountry() &&
                    d.PhoneNumber.IsValidPhoneNumber() &&
                    d.BankAccountNumber.IsValidBankAccountNumber() &&
                    d.PayPerHour.IsValidPayPerHour()))
                .CustomAsync(async (paramedicDTOs, context, cancellationToken) =>
                {
                    var paramedicIds = paramedicDTOs.Select(d => d.Id).Distinct().ToArray();

                    if (paramedicIds.Length != paramedicDTOs.Length)
                    {
                        context.AddFailure("Request.Paramedics", "One or more paramedics are duplicated");
                        return;
                    }

                    var paramedicsExist = dbContext.Paramedics
                        .Count(d => paramedicIds.Contains(d.Id)).Equals(paramedicDTOs.Length);

                    if (!paramedicsExist)
                        context.AddFailure("Request.Paramedics", "One or more paramedics do not exist");
                })
                .When(command => command.Paramedics != null && command.Paramedics.Any())
                .NotNull().WithMessage("{PropertyName} cannot be null");
        }
    }
}