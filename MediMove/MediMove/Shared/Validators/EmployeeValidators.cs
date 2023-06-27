
using System.Text.RegularExpressions;

namespace MediMove.Shared.Validators;

public static class EmployeeValidators
{
    private static readonly Regex BankAccountNumberPattern = new (@"^[0-9]{6,26}$");
    public static bool IsValidBankAccountNumber(this string value) =>
        BankAccountNumberPattern.IsMatch(value);
}
