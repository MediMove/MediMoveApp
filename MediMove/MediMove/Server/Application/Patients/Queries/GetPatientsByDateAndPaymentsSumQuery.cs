using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Queries
{
    public record GetPatientsByDateAndPaymentsSumQuery(
        DateTime StartDate,
        DateTime EndDate,
        decimal StartAmount,
        decimal EndAmount)
        : IRequest<ErrorOr<GetPatientsByDateAndPaymentsSumDTO>>;
}