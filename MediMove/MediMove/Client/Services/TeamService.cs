using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.Enums;
using System.Net.Http.Formatting;
using MediMove.Client.temp;
using Newtonsoft.Json;
using ErrorOr;

namespace MediMove.Client.Services
{
    public class TeamService : BaseService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public TeamService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }
        public async Task<GetTeamsByDateAndShiftResponse> GetTeamsByDayAndShift(DateTime dateTime, ShiftType shift)
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Team");

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
            var result = await response.Content.ReadFromJsonAsync<GetTeamsByDateAndShiftResponse>();
            return result;
        }

        public async Task<ErrorOr<string>> PostTeam(CreateTeamsRequest content)
        {

            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Team");
            Console.WriteLine($"I'm here {requestUri}");
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            request.Content = new ObjectContent<CreateTeamsRequest>(content, new JsonMediaTypeFormatter());

            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            return await DeserializeError(response);
        }

    }
}
