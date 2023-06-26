using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Formatting;
using ErrorOr;
using MediatR;

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
          
            var request = await GenerateRequestAsync("/api/v1/Availability", HttpMethod.Post);

            request.Content = new ObjectContent<CreateAvailabilitiesRequest>(content, new JsonMediaTypeFormatter());

            var response = await _httpClient.SendAsync(request);
            
            return await UnpackResponse<Unit>(response);
        }
    }
}
