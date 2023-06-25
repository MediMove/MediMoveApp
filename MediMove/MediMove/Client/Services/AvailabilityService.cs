using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace MediMove.Client.Services
{
    public class AvailabilityService : BaseService
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

        public async Task<string> SaveAvailabilities(Dictionary<DateTime, ShiftType?> availabilities)
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
            return await DeserializeError(response);

        }
    }
}
