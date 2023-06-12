using MediMove.Shared.Models.DTOs;
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

        public TransportService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<IEnumerable<TransportDTO>> GetTransportByDay(int day, int month, int year)
        {

            var uriBuilder = new UriBuilder("/api/v1/paramedic"); 

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

            return result;




            //var build = new UriBuilder()//($"{_httpClient.BaseAddress}/api/paramedic/");
            //build.Query
            //var uri = new Uri($"{_httpClient.BaseAddress}/api/paramedic/", );
        }
    }
}
