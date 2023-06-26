using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MediMove.Shared.Models.Enums;
using ErrorOr;
using MediatR;

namespace MediMove.Client.Services
{
    public class TransportService: BaseService
    {
        public TransportService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }

        public async Task<ErrorOr<GetTransportsByParamedicAndDateRangeResponse.TransportInfo[]>> GetTransportByDay(int day, int month, int year)
        {
            var uriBuilder = GenerateUriBuilder("api/v1/Transport/Paramedic");
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["startDateInclusive"] = new DateTime(year, month, day).ToString("yyyy-MM-dd"); //dowolność 
            query["endDateInclusive"] = new DateTime(year, month, day).ToString("yyyy-MM-dd");

            //można poszerzyć zakres dat i flitrować ją,żeby nie strzelać do api za każdym razem

            uriBuilder.Query = query.ToString();
            var result =  await HandleQueryAsync<GetTransportsByParamedicAndDateRangeResponse>(uriBuilder, HttpMethod.Get);
            if (result.IsError)
                return result.Errors;
            
            result.Value.Transports.TryGetValue(new DateTime(year, month, day), out GetTransportsByParamedicAndDateRangeResponse.TransportInfo[] toReturn);
            return toReturn;
        }

        public async Task<ErrorOr<GetTransportsByDayAndShiftResponse>> GetTransportByDayAndShift(DateTime dateTime, ShiftType shift)
        {
            var uriBuilder = GenerateUriBuilder("api/v1/Transport/Date");
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["date"] = dateTime.ToString("yyyy-MM-dd");
            Console.WriteLine($"Date query: {query["date"]}");
            if (shift == ShiftType.Morning)
                query["shift"] = "0";
            else if (shift == ShiftType.Evening)
                query["shift"] = "1";

            Console.WriteLine($"Shift query: {query["shift"]}");

            uriBuilder.Query = query.ToString();
            return await HandleQueryAsync<GetTransportsByDayAndShiftResponse>(uriBuilder, HttpMethod.Get);
        }

        public async Task<ErrorOr<Unit>> PostTransport(CreateTransportDTO content) =>
            await HandleRequestAsync<CreateTransportDTO, Unit>("api/v1/Transport", HttpMethod.Post, content);
       
        public async Task<ErrorOr<Unit>> PostTransportWithBilling(CreateTransportWithBillingDTO content) =>
            await HandleRequestAsync<CreateTransportWithBillingDTO, Unit>("api/v1/Transport/WithBilling", HttpMethod.Post, content);

        public async Task<ErrorOr<Unit>> AddTeamToTransport(AssignTeamsToTransportsRequest content) =>
            await HandleRequestAsync<AssignTeamsToTransportsRequest, Unit>("api/v1/Transport/AssignTeams", HttpMethod.Post, content);
    }
}
