using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ErrorOr;
using MediatR;
using static MediMove.Shared.Models.DTOs.GetAvailabilitiesForParamedicByDateRangeResponse;

namespace MediMove.Client.Services
{
    public class AvailabilityService : BaseService
    {
        public AvailabilityService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }

        public async Task<ErrorOr<Unit>> SaveAvailabilities(Dictionary<DateTime, ShiftType?> availabilities)
        {
            var content = new CreateAvailabilitiesRequest(availabilities);
            return await HandleRequestAsync<CreateAvailabilitiesRequest, Unit>("api/v1/Availability", HttpMethod.Post, content);
        }

        public async Task<ErrorOr<Unit>> DeleteAvailabilities(HashSet<DateTime> hashSet)
        {
            var content = new DeleteAvailabilitiesRequest(hashSet);
            return await HandleRequestAsync<DeleteAvailabilitiesRequest, Unit>("api/v1/Availability", HttpMethod.Delete, content);
        }

        public async Task<ErrorOr<Dictionary<DateTime, DateInfo>>> GetAvailabilities()
        {
            var result = await HandleRequestAsync<GetAvailabilitiesForParamedicByDateRangeResponse>("api/v1/Availability/Paramedic", HttpMethod.Get);

            return result.Match<ErrorOr<Dictionary<DateTime, DateInfo>>>(
                response => response.DateToDateInfo,
                errors => errors);
        }
    }
}