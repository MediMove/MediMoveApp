using System.Text.RegularExpressions;

namespace MediMove.Shared.Validators;

public static class AccountsValidators
{
    private static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

    private static bool IsSpecialChar(char c)
    {
        char[] specialChars = { '!', '@', '#', '$', '%', '^', '&', '*' };
        return specialChars.Contains(c);
    }

    public static bool IsValidPassword(this string value)
    {
        bool hasUpperCase = value.Any(char.IsUpper);
        bool hasLowerCase = value.Any(char.IsLower);
        bool hasDigit = value.Any(char.IsDigit);
        bool hasSpecialChar = value.Any(IsSpecialChar);
        bool isLongEnough = value.Length >= 8;

        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar && isLongEnough;
    }

    public static bool IsValidEmail(this string value) =>
        !string.IsNullOrEmpty(value) && EmailRegex.IsMatch(value);
}