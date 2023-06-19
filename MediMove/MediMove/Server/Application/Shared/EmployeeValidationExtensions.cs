using FluentValidation;
using MediMove.Shared.Validators;

namespace MediMove.Server.Application.Shared;
/// <summary>
/// Extensions for validating employees.
/// </summary>
public static class EmployeeValidationExtensions
{
    public static IRuleBuilderOptions<T, string> BankAccountNumber<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidBankAccountNumber()).WithMessage("Invalid {PropertyName}");
}
