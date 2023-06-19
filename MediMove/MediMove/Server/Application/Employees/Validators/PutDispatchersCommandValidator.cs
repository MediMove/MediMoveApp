using FluentValidation;
using MediMove.Server.Application.Employees.Commands;
using MediMove.Server.Data;
using MediMove.Shared.Validators;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Employees.Validators
{
    /// <summary>
    /// Validator for <see cref="PutDispatchersCommand"/>.
    /// </summary>
    public class PutDispatchersCommandValidator : AbstractValidator<PutDispatchersCommand>
    {
        /// <summary>
        /// Constructor for <see cref="PutDispatchersCommandValidator"/>.
        /// </summary>
        /// <param name="dbContext"><see cref="MediMoveDbContext"/></param>
        public PutDispatchersCommandValidator(MediMoveDbContext dbContext)
        {
            RuleFor(command => command.Dispatchers)
                .Must(dispatchers => dispatchers.All(d =>
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
                    d.Salary.IsValidSalary()))
                .CustomAsync(async (dispatcherDTOs, context, cancellationToken) =>
                {
                    var dispatcherIds = dispatcherDTOs.Select(d => d.Id).Distinct().ToArray();

                    if (dispatcherIds.Length != dispatcherDTOs.Length)
                    {
                        context.AddFailure("Dispatchers", "One or more dispatchers are duplicated");
                        return;
                    }
                    
                    var dispatchersExist = dbContext.Dispatchers
                        .Count(d => dispatcherIds.Contains(d.Id)).Equals(dispatcherDTOs.Length);

                    if (!dispatchersExist)
                        context.AddFailure("Dispatchers", "One or more dispatchers do not exist");
                })
                .When(command => command.Dispatchers != null && command.Dispatchers.Any())
                .NotNull().WithMessage("{PropertyName} cannot be null");
        }
    }
}