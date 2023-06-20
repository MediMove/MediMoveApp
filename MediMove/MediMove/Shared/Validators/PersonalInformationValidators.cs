using System.Text.RegularExpressions;

namespace MediMove.Shared.Validators;

public static class PersonalInformationValidators
{
    private static readonly Regex NameRegex = new(@"^[\p{L}\s-]+$");
    private static readonly Regex HouseNumberRegex = new(@"^[\p{L}\p{N}\s-]+$");
    private static readonly Regex PostalCodeRegex = new(@"^\d{2}-\d{3}(\d{2})?$");
    private static readonly Regex PhoneNumberRegex = new(@"^[0-9]+([- ]?[0-9]+)*$");

    public static bool IsValidFirstName(this string value) =>
            !string.IsNullOrEmpty(value) && value.Length >= 2 && value.Length <= 25 && NameRegex.IsMatch(value);

    public static bool IsValidLastName(this string value) =>
        !string.IsNullOrEmpty(value) && value.Length >= 2 && value.Length <= 25 && NameRegex.IsMatch(value);

    public static bool IsValidStreetAddress(this string value) =>
        !string.IsNullOrEmpty(value) && value.Length >= 2 && value.Length <= 30 && NameRegex.IsMatch(value);

    public static bool IsValidHouseNumber(this string value) =>
        !string.IsNullOrEmpty(value) && value.Length >= 1 && value.Length <= 10 && HouseNumberRegex.IsMatch(value);

    public static bool IsValidApartmentNumber(this int? value) =>
        !value.HasValue || value <= 200;

    public static bool IsValidPostalCode(this string value) =>
        !string.IsNullOrEmpty(value) && PostalCodeRegex.IsMatch(value);

    public static bool IsValidStateProvince(this string value) =>
        !string.IsNullOrEmpty(value) && value.Length >= 2 && value.Length <= 30 && NameRegex.IsMatch(value);

    public static bool IsValidCity(this string value) =>
        !string.IsNullOrEmpty(value) && value.Length >= 2 && value.Length <= 30 && NameRegex.IsMatch(value);

    public static bool IsValidCountry(this string value) =>
        !string.IsNullOrEmpty(value) && value.Length >= 2 && value.Length <= 30 && NameRegex.IsMatch(value);

    public static bool IsValidPhoneNumber(this string value) =>
        !string.IsNullOrEmpty(value) && value.Length >= 2 && value.Length <= 30 && PhoneNumberRegex.IsMatch(value);
}