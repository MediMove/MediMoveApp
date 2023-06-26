using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Formatting;
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
            var request = await GenerateRequestAsync("api/v1/Availability", HttpMethod.Post);
            var content = new CreateAvailabilitiesRequest(availabilities);
            request.Content = new ObjectContent<CreateAvailabilitiesRequest>(content, new JsonMediaTypeFormatter());

            var response = await _httpClient.SendAsync(request);
            return await UnpackResponse<Unit>(response);
        }

        public async Task<ErrorOr<Unit>> DeleteAvailabilities(HashSet<DateTime> hashSet)
        {
            var request = await GenerateRequestAsync("api/v1/Availability", HttpMethod.Delete);
            var content = new DeleteAvailabilitiesRequest(hashSet);
            request.Content = new ObjectContent<DeleteAvailabilitiesRequest>(content, new JsonMediaTypeFormatter()); // to działa ale jest tu dodatkowy nuGet, jak wysłać po prostu klase?

            var response = await _httpClient.SendAsync(request);
            return await UnpackResponse<Unit>(response);
        }

        public async Task<ErrorOr<Dictionary<DateTime, DateInfo>>> GetAvailabilities()
        {
            var request = await GenerateRequestAsync("api/v1/Availability/Paramedic", HttpMethod.Get);
            var response = await _httpClient.SendAsync(request);

            var result = await UnpackResponse<GetAvailabilitiesForParamedicByDateRangeResponse>(response);
            return result.Match<ErrorOr<Dictionary<DateTime, DateInfo>>>(
                response => response.DateToDateInfo,
                errors => errors);
        }
    }
}