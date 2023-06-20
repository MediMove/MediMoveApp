using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using MediMove.Shared.Extensions;

namespace MediMove.Client.Services
{
    public class TeamService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public TeamService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }
        public async Task<GetTeamsByDateAndShiftResponse> GetTeamsByDAyAndShift(DateTime dateTime)
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Team");

            var uriBuilder = new UriBuilder(requestUri);
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["date"] = dateTime.ToString("yyyy-MM-dd");
            query["shift"] = dateTime.ToShiftType().ToString();


            uriBuilder.Query = query.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");


            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Rzuca wyjątek w przypadku niepowodzenia
            Console.WriteLine(response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<GetTeamsByDateAndShiftResponse>();
            return result;
        }
    }
}
