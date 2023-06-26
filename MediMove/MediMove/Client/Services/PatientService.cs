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
            var uriBuilder = GenerateUriBuilder("api/v1/Patient/Report");
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

            query["startTime"] = start.ToString("yyyy-MM-dd");
            query["endTime"] = end.ToString("yyyy-MM-dd");
            query["startPaymentsSum"] = startAmount.ToString();
            query["endPaymentsSum"] = endAmount.ToString();

            uriBuilder.Query = query.ToString();
            return await HandleQueryAsync<GetPatientsByDateAndPaymentsSumDTO>(uriBuilder, HttpMethod.Get);
        }

        public async Task<ErrorOr<Unit>> PostPatient(CreatePatientRequest content) =>
            await HandleRequestAsync<CreatePatientRequest, Unit>("api/v1/Patient", HttpMethod.Post, content);
    }
}
