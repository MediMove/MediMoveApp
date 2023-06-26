using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Validators;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MediMove.Client.Services
{
    public class EmployeeService : BaseService
    {
        public EmployeeService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }

        private static string? ValidateEmployee(EmployeeDTO employee)
        {
            if (!employee.FirstName.IsValidFirstName())
                return "Invalid first name";

            if (!employee.LastName.IsValidLastName())
                return "Invalid last name";

            if (!employee.Email.IsValidEmail())
                return "Invalid email";

            if (!employee.StreetAddress.IsValidStreetAddress())
                return "Invalid street address";

            if (!employee.HouseNumber.IsValidHouseNumber())
                return "Invalid house number";

            if (!employee.ApartmentNumber.IsValidApartmentNumber())
                return "Invalid apartment number";

            if (!employee.PostalCode.IsValidPostalCode())
                return "Invalid postal code";

            if (!employee.City.IsValidCity())
                return "Invalid city";

            if (!employee.StateProvince.IsValidStateProvince())
                return "Invalid state/province";

            if (!employee.Country.IsValidCountry())
                return "Invalid country";

            if (!employee.PhoneNumber.IsValidPhoneNumber())
                return "Invalid phone number";

            if (!employee.BankAccountNumber.IsValidBankAccountNumber())
                return "Invalid bank account number";

            if (employee is ParamedicDTO paramedic && !paramedic.PayPerHour.IsValidPayPerHour())
                return "Invalid pay per hour";

            if (employee is DispatcherDTO dispatcher && !dispatcher.Salary.IsValidSalary())
                return "Invalid salary";
            
            return null;
        }


        public async Task<ErrorOr<EmployeeDTO[]>> GetAllEmployees()
        {
            var request = await GenerateRequestAsync("api/v1/Employee", HttpMethod.Get);

            var response = await _httpClient.SendAsync(request);

            var result = await UnpackResponse<GetAllEmployeesResponse>(response);

            if (result.IsError)
                return result.Errors;


            var paramedics = result.Value.Paramedics;
            var dispatchers = result.Value.Dispatchers;

            // merge paramedics and dispatchers
            var toReturn = new EmployeeDTO[paramedics.Length + dispatchers.Length];
            paramedics.CopyTo(toReturn, 0);
            dispatchers.CopyTo(toReturn, paramedics.Length);

            return toReturn;
        }

        public async Task<ErrorOr<Unit>> UpdateEmployees(List<EmployeeDTO> employees)
        {
            var paramedics = new List<ParamedicDTO>();
            var dispatchers = new List<DispatcherDTO>();
            foreach (var employee in employees)
            {
                var errorMessage = ValidateEmployee(employee);
                if (errorMessage is not null) return Error.Failure(errorMessage);
                if (employee is ParamedicDTO paramedic)
                    paramedics.Add(paramedic);
                else if (employee is DispatcherDTO dispatcher)
                    dispatchers.Add(dispatcher);
            }

            var request = await GenerateRequestAsync("api/v1/Employee", HttpMethod.Put);

            string serializedRequest = JsonSerializer.Serialize(new PutEmployeesRequest(paramedics.ToArray(), dispatchers.ToArray()));
            request.Content = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);

            return await UnpackResponse<Unit>(response);
        }
    }
}
