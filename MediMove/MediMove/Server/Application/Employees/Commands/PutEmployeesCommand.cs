using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Employees.Commands;

/// <summary>
/// Command for updating employees.
/// </summary>
/// <param name="Request"><see cref="PutEmployeesRequest"/></param>
public record PutEmployeesCommand(PutEmployeesRequest Request) : IRequest<ErrorOr<Unit>>;