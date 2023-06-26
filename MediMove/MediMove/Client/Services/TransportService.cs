using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using MediMove.Client.temp;
using MediMove.Shared.Models.Enums;
using System;
using MediMove.Shared.Extensions;

using ErrorOr;
using MediatR;


namespace MediMove.Client.Services
{
    public class TransportService: BaseService
    {
      

        public TransportService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }


        public async Task<GetTransportsByParamedicAndDateRangeResponse.TransportInfo[]> GetTransportByDay(int day, int month, int year)
        {

            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Transport/Paramedic");

            //"https://localhost:7244/api/v1/paramedic?day=10&month=6&year=2023");//
            var uriBuilder = new UriBuilder(requestUri); 

            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            //query["startDateInclusive"] = $"{year}-{month}-{day}";
            //query["endDateInclusive"] = $"{year}-{month}-{day}";  //dowolność 

            query["startDateInclusive"] = new DateTime(year, month, day).ToString("yyyy-MM-dd"); //dowolność 
            query["endDateInclusive"] = new DateTime(year, month, day).ToString("yyyy-MM-dd");

            //można poszerzyć zakres dat i flitrować ją,żeby nie strzelać do api za każdym razem

            uriBuilder.Query = query.ToString();
          
            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            
            
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode(); // Rzuca wyjątek w przypadku niepowodzenia
            Console.WriteLine(response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<GetTransportsByParamedicAndDateRangeResponse>();

            //var result = await response.Content.ReadAsStringAsync();
            
            result.Transports.TryGetValue(new DateTime(year, month, day), out GetTransportsByParamedicAndDateRangeResponse.TransportInfo[] toReturn);

            return toReturn;

        }

        public async Task<GetTransportsByDayAndShiftResponse> GetTransportByDayAndShift(DateTime dateTime, ShiftType shift)
        {

            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Transport/Date");
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
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<GetTransportsByDayAndShiftResponse>();
            return result;

        }

        public async Task<ErrorOr<Unit>> PostTransport(CreateTransportDTO content)
        {

            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Transport");
            Console.WriteLine($"I'm here {requestUri}");
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            request.Content = new ObjectContent<CreateTransportDTO>(content, new JsonMediaTypeFormatter());

            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            return await UnpackResponse<Unit>(response);
        }

        public async Task<ErrorOr<Unit>> PostTransportWithBilling(CreateTransportWithBillingDTO content)
        {

            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Transport/WithBilling");
            Console.WriteLine($"I'm here {requestUri}");
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            request.Content = new ObjectContent<CreateTransportWithBillingDTO>(content, new JsonMediaTypeFormatter());

            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);
            return await UnpackResponse<Unit>(response);
        }

        public async Task<ErrorOr<Unit>> AddTeamToTransport(AssignTeamsToTransportsRequest content)
        {
            var baseUri = new Uri(_navigationManager.BaseUri);
            var requestUri = new Uri(baseUri, "/api/v1/Transport/AssignTeams");
            Console.WriteLine($"I'm here {requestUri}");
            var request = new HttpRequestMessage(HttpMethod.Patch, requestUri);

            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            request.Content = new ObjectContent<AssignTeamsToTransportsRequest>(content, new JsonMediaTypeFormatter());

            Console.WriteLine("I'm here auth");
            var response = await _httpClient.SendAsync(request);

            return await UnpackResponse<Unit>(response);
        }

    }
}
