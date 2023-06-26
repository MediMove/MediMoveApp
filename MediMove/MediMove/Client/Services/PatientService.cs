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

        public async Task<ErrorOr<string>> PostPatient(CreatePatientRequest content)
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
            return await DeserializeError(response);
        }

        
    }
}
