using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Validators;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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
            var result = await HandleRequestAsync<GetAllEmployeesResponse>("api/v1/Employee", HttpMethod.Get);

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

        public async Task<GetEmployeesInMonthByHoursAndSalaryDTO> GetEmployeesReport(DateTime start, DateTime end, decimal startAmount, decimal endAmount, decimal startHours, decimal endHours)
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Employee/Report/Multiple");

            Console.WriteLine($"I'm here {requestUri}");
            var uriBuilder = new UriBuilder(requestUri);

            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["startTime"] = start.ToString("yyyy-MM-dd");
            query["endTime"] = end.ToString("yyyy-MM-dd");
            query["startPaymentsSum"] = startAmount.ToString();
            query["endPaymentsSum"] = endAmount.ToString();
            query["startPaymentsHours"] = startHours.ToString();
            query["endPaymentsHours"] = endHours.ToString();

            uriBuilder.Query = query.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            //var content = await response.Content.ReadAsStringAsync();

            //// Deserialize the JSON content to the GetEmployeesInMonthByHoursAndSalaryDTO object
            //var options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true // Enable case-insensitive property matching
            //};

            //var result = JsonSerializer.Deserialize<GetEmployeesInMonthByHoursAndSalaryDTO>(content, options);


            var result = await response.Content.ReadFromJsonAsync<GetEmployeesInMonthByHoursAndSalaryDTO>();
            Console.WriteLine(result);
            var paramedics = result;

            // merge paramedics and dispatchers
            //var toReturn = new EmployeeDTO[paramedics.Length + dispatchers.Length];
            //paramedics.CopyTo(toReturn, 0);
            //dispatchers.CopyTo(toReturn, paramedics.Length);
            //foreach (var employee in paramedics)
            //{
            //    Console.WriteLine(employee);
            //}

            return paramedics;
        }
        public async Task<GetEmployeeRatesByIdAndDatesDTO> GetParamedicReport(int id, DateTime start, DateTime end)
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Employee/Report/Single");

            Console.WriteLine($"I'm here {requestUri}");
            var uriBuilder = new UriBuilder(requestUri);

            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["id"] = id.ToString();
            query["startTime"] = start.ToString("yyyy-MM-dd");
            query["endTime"] = end.ToString("yyyy-MM-dd");
           
            uriBuilder.Query = query.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            //var content = await response.Content.ReadAsStringAsync();

            //// Deserialize the JSON content to the GetEmployeesInMonthByHoursAndSalaryDTO object
            //var options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true // Enable case-insensitive property matching
            //};

            //var result = JsonSerializer.Deserialize<GetEmployeesInMonthByHoursAndSalaryDTO>(content, options);


            var result = await response.Content.ReadFromJsonAsync<GetEmployeeRatesByIdAndDatesDTO>();
            Console.WriteLine(result);
            var paramedics = result;

            // merge paramedics and dispatchers
            //var toReturn = new EmployeeDTO[paramedics.Length + dispatchers.Length];
            //paramedics.CopyTo(toReturn, 0);
            //dispatchers.CopyTo(toReturn, paramedics.Length);
            //foreach (var employee in paramedics)
            //{
            //    Console.WriteLine(employee);
            //}

            return paramedics;
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

            return await HandleRequestAsync<PutEmployeesRequest, Unit>("api/v1/Employee", HttpMethod.Put,
                new PutEmployeesRequest(paramedics.ToArray(), dispatchers.ToArray()));
        }

        public async Task<ErrorOr<Unit>> Register(RegisterAdminRequest content) =>
            await HandleRequestAsync<RegisterAdminRequest, Unit>("api/v1/Accounts/Register/Admin", HttpMethod.Post, content);

        public async Task<ErrorOr<Unit>> Register(RegisterParamedicRequest content) =>
            await HandleRequestAsync<RegisterParamedicRequest, Unit>("api/v1/Accounts/Register/Paramedic", HttpMethod.Post, content);

        public async Task<ErrorOr<Unit>> Register(RegisterDispatcherRequest content) =>
            await HandleRequestAsync<RegisterDispatcherRequest, Unit>("api/v1/Accounts/Register/Dispatcher", HttpMethod.Post, content);
    }
}
