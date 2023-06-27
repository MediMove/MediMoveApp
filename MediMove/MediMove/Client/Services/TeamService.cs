using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MediMove.Shared.Models.Enums;
using ErrorOr;
using MediatR;

namespace MediMove.Client.Services
{
    public class TeamService : BaseService
    {
        public TeamService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }
        public async Task<ErrorOr<GetTeamsByDateAndShiftResponse>> GetTeamsByDayAndShift(DateTime dateTime, ShiftType shift)
        {
            var uriBuilder = GenerateUriBuilder("api/v1/Team");
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["date"] = dateTime.ToString("yyyy-MM-dd");
            Console.WriteLine($"Date query: {query["date"]}");
            if (shift == ShiftType.Morning)
                query["shift"] = "0";
            else if (shift == ShiftType.Evening)
                query["shift"] = "1";
            Console.WriteLine($"Shift query: {query["shift"]}");

            uriBuilder.Query = query.ToString();
            return await HandleQueryAsync<GetTeamsByDateAndShiftResponse>(uriBuilder, HttpMethod.Get);
        }

        public async Task<ErrorOr<Unit>> PostTeam(CreateTeamsRequest content) =>
            await HandleRequestAsync<CreateTeamsRequest, Unit>("api/v1/Team", HttpMethod.Post, content);

        public async Task<ErrorOr<Unit>> DeleteTeam(DeleteTeamsRequest content) =>
            await HandleRequestAsync<DeleteTeamsRequest, Unit>("api/v1/Team", HttpMethod.Delete, content);
    }
}