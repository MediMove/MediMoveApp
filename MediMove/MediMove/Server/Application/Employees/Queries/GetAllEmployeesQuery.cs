using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Employees.Queries;

/// <summary>
/// Query for getting all employees.
/// </summary>
/// <param name="IsWorking">Specifies whether to filter employees by their working status</param>
public record GetAllEmployeesQuery(bool? IsWorking = null) : IRequest<ErrorOr<GetAllEmployeesResponse>>;