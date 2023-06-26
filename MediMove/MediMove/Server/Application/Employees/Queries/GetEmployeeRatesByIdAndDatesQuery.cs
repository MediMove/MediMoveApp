using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Employees.Queries

{
    public record GetEmployeeRatesByIdAndDatesQuery(
        int id,
        DateTime StartDate,
        DateTime EndDate
        //decimal StartAmount,
        //decimal EndAmount
        )
        : IRequest<ErrorOr<GetEmployeeRatesByIdAndDatesDTO>>;
}
