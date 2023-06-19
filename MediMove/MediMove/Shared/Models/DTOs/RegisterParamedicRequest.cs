namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// Request for registering a paramedic.
/// </summary>
/// <param name="Email">email</param>
/// <param name="Password">password</param>
/// <param name="FirstName">first name</param>
/// <param name="LastName">last name</param>
/// <param name="StreetAddress">street address</param>
/// <param name="HouseNumber">house number</param>
/// <param name="ApartmentNumber">apartment number</param>
/// <param name="City">city</param>
/// <param name="PostalCode">postal code</param>
/// <param name="StateProvince">state province</param>
/// <param name="Country">country</param>
/// <param name="PhoneNumber">phone number</param>
/// <param name="BankAccountNumber">bank account number</param>
/// <param name="IsDriver">bool indicating if paramedic is a driver</param>
/// <param name="PayPerHour">starting pay per hour</param>
/// <param name="IsWorking">bool indicating if paramedic is currently working at company</param>
public record RegisterParamedicRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string StreetAddress,
    string HouseNumber,
    int? ApartmentNumber,
    string City,
    string PostalCode,
    string StateProvince,
    string Country,
    string PhoneNumber,
    string BankAccountNumber,
    bool IsDriver,
    decimal PayPerHour,
    bool IsWorking = true
);