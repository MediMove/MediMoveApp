using MediMove.Client.temp;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Validators;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MediMove.Client.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        private string ValidateEmployee(EmployeeDTO employee)
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

            return "";
        }

        public EmployeeService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }

        public async Task<EmployeeDTO[]> GetAllEmployees()
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Employee");

            var uriBuilder = new UriBuilder(requestUri);

            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GetAllEmployeesResponse>();
            Console.WriteLine(result);
            var paramedics = result.Paramedics;
            var dispatchers = result.Dispatchers;

            // merge paramedics and dispatchers
            var toReturn = new EmployeeDTO[paramedics.Length + dispatchers.Length];
            paramedics.CopyTo(toReturn, 0);
            dispatchers.CopyTo(toReturn, paramedics.Length);
            foreach (var employee in toReturn)
            {
                Console.WriteLine(employee);
            }

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

        public async Task<string> UpdateEmployees(List<EmployeeDTO> employees)
        {
            var paramedics = new List<ParamedicDTO>();
            var dispatchers = new List<DispatcherDTO>();
            foreach (var employee in employees)
            {
                //Console.WriteLine(employee);
                var errorMessage = ValidateEmployee(employee);
                if (errorMessage != "") return $"{errorMessage} for ID={employee.Id}";
                if (employee is ParamedicDTO paramedic)
                    paramedics.Add(paramedic);
                else if (employee is DispatcherDTO dispatcher)
                    dispatchers.Add(dispatcher);
            }

            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Employee");

            var uriBuilder = new UriBuilder(requestUri);

            var request = new HttpRequestMessage(HttpMethod.Put, uriBuilder.ToString());

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string serializedRequest = JsonSerializer.Serialize(new PutEmployeesRequest(paramedics.ToArray(), dispatchers.ToArray()));
            request.Content = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                //Console.WriteLine("Error updating data");
                return "Error updating data";
                /*
                var responseContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
                var errorMessage = "";
                foreach (var field in errorResponse.Errors)
                {
                    errorMessage += field.Key + ": " + string.Join("; ", field.Value) + "\n";
                }

                return errorMessage;
                */
            }
            //Console.WriteLine("OK updating data");
            return "";
        }
    }
}
