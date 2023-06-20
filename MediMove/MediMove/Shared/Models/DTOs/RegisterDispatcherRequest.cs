namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// 
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
/// <param name="Salary">starting salary</param>
/// <param name="IsWorking">bool indicating if dispatcher is currently working at company</param>
public class RegisterDispatcherRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string StreetAddress { get; set; }
    public string HouseNumber { get; set; }
    public int? ApartmentNumber { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string StateProvince { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
    public decimal Salary { get; set; }
    public bool IsWorking { get; set; } = true;
}

