using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Employees.Queries
{
    public record GetEmployeesInMonthByHoursAndSalaryQuery(
        DateTime StartDate,
        DateTime EndDate,
        decimal StartHours,
        decimal EndHours,
        decimal StartAmount,
        decimal EndAmount)
        : IRequest<ErrorOr<GetEmployeesInMonthByHoursAndSalaryDTO>>;
}