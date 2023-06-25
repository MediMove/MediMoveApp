using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MediMove.Client.Services
{
    public class ParamedicService : BaseService
    {

        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public ParamedicService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }

        public async Task<GetAvailableParamedicsByDateAndShiftResponse> GetParamedicsByDayAndShift(DateTime dateTime, ShiftType shift)
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Availability");

            var uriBuilder = new UriBuilder(requestUri);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["date"] = dateTime.ToString("yyyy-MM-dd");
            Console.WriteLine($"Date query: {query["date"]}");
            if (shift == ShiftType.Morning)
                query["shift"] = "0";
            else if (shift == ShiftType.Evening)
                query["shift"] = "1";
            Console.WriteLine($"Shift query: {query["shift"]}");


            uriBuilder.Query = query.ToString();
            Console.WriteLine(uriBuilder.ToString());
            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");


            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Rzuca wyjątek w przypadku niepowodzenia
            Console.WriteLine(response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<GetAvailableParamedicsByDateAndShiftResponse>();
            return result;
        }
    }
}
