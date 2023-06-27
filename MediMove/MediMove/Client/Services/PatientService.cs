using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ErrorOr;
using MediatR;

namespace MediMove.Client.Services
{
    public class PatientService : BaseService
    {

        public PatientService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager) : base(httpClient, jsRuntime, navigationManager)
        {
        }

        public async Task<ErrorOr<GetAllPatientsResponse>> GetPatients() =>
            await HandleRequestAsync<GetAllPatientsResponse>("api/v1/Patient", HttpMethod.Get);

        public async Task<ErrorOr<GetPatientsByDateAndPaymentsSumDTO>> GetPatientsReport(DateTime start, DateTime end, decimal startAmount, decimal endAmount)
        {
            var content = new GetPatientsByDateAndPaymentsSumRequest(start, end, startAmount, endAmount);
            return await HandleRequestAsync<GetPatientsByDateAndPaymentsSumRequest, GetPatientsByDateAndPaymentsSumDTO>("api/v1/Patient/Report", HttpMethod.Post, content);
        }

        public async Task<ErrorOr<Unit>> PostPatient(CreatePatientRequest content) =>
            await HandleRequestAsync<CreatePatientRequest, Unit>("api/v1/Patient", HttpMethod.Post, content);
    }
}
