using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Formatting;
using ErrorOr;
using MediatR;
using static MediMove.Shared.Models.DTOs.GetAvailabilitiesForParamedicByDateRangeResponse;
using System.Net.Http.Json;
using System;
using Azure.Core;

namespace MediMove.Client.Services
{
    public class AvailabilityService : BaseService
    {
        
        public AvailabilityService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }

        public async Task<ErrorOr<Unit>> SaveAvailabilities(Dictionary<DateTime, ShiftType?> availabilities)
        {
            var content = new CreateAvailabilitiesRequest(availabilities);
          
            var request = await GenerateRequestAsync("/api/v1/Availability", HttpMethod.Post);

            request.Content = new ObjectContent<CreateAvailabilitiesRequest>(content, new JsonMediaTypeFormatter());

            var response = await _httpClient.SendAsync(request);
            
            return await UnpackResponse<Unit>(response);

        }

        public async Task DeleteAvailabilities(HashSet<DateTime> hashSet)
        {
            var content = new DeleteAvailabilitiesRequest(hashSet);
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Availability");
            Console.WriteLine($"I'm here {requestUri}");
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            request.Content = new ObjectContent<DeleteAvailabilitiesRequest>(content, new JsonMediaTypeFormatter()); // to działa ale jest tu dodatkowy nuGet, jak wysłać po prostu klase?

            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            Console.WriteLine(response.StatusCode);
            response.EnsureSuccessStatusCode(); // Rzuca wyjątek w przypadku niepowodz
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
