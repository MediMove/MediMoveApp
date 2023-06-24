using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Formatting;
using static MediMove.Shared.Models.DTOs.GetAvailabilitiesForParamedicByDateRangeResponse;
using System.Net.Http.Json;
using System;

namespace MediMove.Client.Services
{
    public class AvailabilityService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public AvailabilityService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }

        public async Task SaveAvailabilities(Dictionary<DateTime, ShiftType?> availabilities)
        {
            var content = new CreateAvailabilitiesRequest(availabilities);
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Availability");
            Console.WriteLine($"I'm here {requestUri}");
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            request.Content = new ObjectContent<CreateAvailabilitiesRequest>(content, new JsonMediaTypeFormatter()); // to działa ale jest tu dodatkowy nuGet, jak wysłać po prostu klase?

            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Rzuca wyjątek w przypadku niepowodzenia
            Console.WriteLine(response.StatusCode);
            //var result = await response.Content.ReadFromJsonAsync<GetAllPatientsResponse>();

        }

        public async Task<Dictionary<DateTime, DateInfo>> GetAvailabilities()
        {


            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Availability/Paramedic");
            var uriBuilder = new UriBuilder(requestUri);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["startDateInclusive"] = null;
            query["endDateInclusive"] = null;

            uriBuilder.Query = query.ToString();


            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Rzuca wyjątek w przypadku niepowodzenia
            var result = await response.Content.ReadFromJsonAsync<GetAvailabilitiesForParamedicByDateRangeResponse>();
            return result.DateToDateInfo;
        }
    }
}
