using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MediMove.Client.Services
{
    public class TransportService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public TransportService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }

        public async Task<IEnumerable<TransportDTO>> GetTransportByDay(int day, int month, int year)
        {

            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/transport/paramedic");

            //"https://localhost:7244/api/v1/paramedic?day=10&month=6&year=2023");//
            var uriBuilder = new UriBuilder(requestUri); 

            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            query["day"] = day.ToString();
            query["month"] = month.ToString();
            query["year"] = year.ToString();

            uriBuilder.Query = query.ToString();
          
            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            
            //Jakiś wyjątek jeśli null?
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode(); // Rzuca wyjątek w przypadku niepowodzenia

            var result = await response.Content.ReadFromJsonAsync<IEnumerable<TransportDTO>>();
            //var result = await response.Content.ReadAsStringAsync();
            return result;




            //var build = new UriBuilder()//($"{_httpClient.BaseAddress}/api/paramedic/");
            //build.Query
            //var uri = new Uri($"{_httpClient.BaseAddress}/api/paramedic/", );
        }
    }
}
