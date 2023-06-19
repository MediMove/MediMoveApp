using FluentValidation;
using MediMove.Shared.Validators;

namespace MediMove.Server.Application.Shared;

public static class PersonalInformationValidationExtensions
{
    public static IRuleBuilderOptions<T, string> FirstName<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidFirstName()).WithMessage("Invalid {PropertyName}");

    public static IRuleBuilderOptions<T, string> LastName<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidLastName()).WithMessage("Invalid {PropertyName}");

    public static IRuleBuilderOptions<T, string> StreetAddress<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidStreetAddress()).WithMessage("Invalid {PropertyName}");

    public static IRuleBuilderOptions<T, string> HouseNumber<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidHouseNumber()).WithMessage("Invalid {PropertyName}");

    public static IRuleBuilderOptions<T, int?> ApartmentNumber<T>(this IRuleBuilder<T, int?> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidApartmentNumber()).WithMessage("Invalid {PropertyName}");

    public static IRuleBuilderOptions<T, string> PostalCode<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidPostalCode()).WithMessage("Invalid {PropertyName}");

    public static IRuleBuilderOptions<T, string> StateProvince<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidStateProvince()).WithMessage("Invalid {PropertyName}");

    public static IRuleBuilderOptions<T, string> City<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidCity()).WithMessage("Invalid {PropertyName}");

    public static IRuleBuilderOptions<T, string> Country<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidCountry()).WithMessage("Invalid {PropertyName}");

    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder.Must(s => s.IsValidPhoneNumber()).WithMessage("Invalid {PropertyName}");
}