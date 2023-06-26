using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System;
using System.Net.Http.Formatting;
using MediMove.Shared.Validators;
using MediMove.Client.temp;
using Newtonsoft.Json;
using ErrorOr;
using MediatR;

namespace MediMove.Client.Services
{
    public class PatientService : BaseService
    {
        
        public PatientService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }

        public async Task<GetAllPatientsResponse> GetPatients()
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Patient");
            Console.WriteLine($"I'm here {requestUri}");
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");


            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Rzuca wyjątek w przypadku niepowodzenia
            Console.WriteLine(response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<GetAllPatientsResponse>();
            return result;
        }

        public async Task<GetPatientsByDateAndPaymentsSumDTO> GetPatientsReport(DateTime start, DateTime end, decimal startAmount, decimal endAmount)
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Patient/Report");

            Console.WriteLine($"I'm here {requestUri}");
            var uriBuilder = new UriBuilder(requestUri);

            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["startTime"] = start.ToString("yyyy-MM-dd");
            query["endTime"] = end.ToString("yyyy-MM-dd");
            query["startPaymentsSum"] = startAmount.ToString();
            query["endPaymentsSum"] = endAmount.ToString();

            uriBuilder.Query = query.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON content to the GetEmployeesInMonthByHoursAndSalaryDTO object
            //var options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true // Enable case-insensitive property matching
            //};

            //var result = JsonSerializer.Deserialize<GetEmployeesInMonthByHoursAndSalaryDTO>(content, options);


            var result = await response.Content.ReadFromJsonAsync<GetPatientsByDateAndPaymentsSumDTO>();
            Console.WriteLine(result);
            var patients = result;

            // merge paramedics and dispatchers
            //var toReturn = new EmployeeDTO[paramedics.Length + dispatchers.Length];
            //paramedics.CopyTo(toReturn, 0);
            //dispatchers.CopyTo(toReturn, paramedics.Length);
            //foreach (var employee in paramedics)
            //{
            //    Console.WriteLine(employee);
            //}

            return patients;
        }


        public async Task<ErrorOr<Unit>> PostPatient(CreatePatientRequest content)
        {
            
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Patient");
            Console.WriteLine($"I'm here {requestUri}");
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            request.Content = new ObjectContent<CreatePatientRequest>(content, new JsonMediaTypeFormatter()); // to działa ale jest tu dodatkowy nuGet, jak wysłać po prostu klase?

            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            return await UnpackResponse<Unit>(response);
        }
    }
}
