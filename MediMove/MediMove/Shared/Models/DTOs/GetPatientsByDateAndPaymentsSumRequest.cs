
namespace MediMove.Shared.Models.DTOs;

public record GetPatientsByDateAndPaymentsSumRequest
(
    DateTime StartTime,
    DateTime EndTime,
    decimal StartPaymentsSum,
    decimal EndPaymentsSum);