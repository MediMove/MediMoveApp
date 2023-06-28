
namespace MediMove.Shared.Models.DTOs;

public record GetEmployeesInMonthByHoursAndSalaryRequest
(
    DateTime StartTime,
    DateTime EndTime,

       decimal StartPaymentsSum,

      decimal EndPaymentsSum,
      decimal StartPaymentsHours,

       decimal EndPaymentsHours
);