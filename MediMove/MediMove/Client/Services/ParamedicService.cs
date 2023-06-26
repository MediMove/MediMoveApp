using ErrorOr;
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

        public ParamedicService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }

        public async Task<ErrorOr<GetAvailableParamedicsByDateAndShiftResponse>> GetParamedicsByDayAndShift(DateTime dateTime, ShiftType shift)
        {
            var uriBuilder = GenerateUriBuilder("api/v1/Availability");
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["date"] = dateTime.ToString("yyyy-MM-dd");
            Console.WriteLine($"Date query: {query["date"]}");
            if (shift == ShiftType.Morning)
                query["shift"] = "0";
            else if (shift == ShiftType.Evening)
                query["shift"] = "1";
            Console.WriteLine($"Shift query: {query["shift"]}");

            uriBuilder.Query = query.ToString();
            return await HandleQueryAsync<GetAvailableParamedicsByDateAndShiftResponse>(uriBuilder, HttpMethod.Get);
        }
    }
}
