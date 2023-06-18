using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Employees.Queries;

/// <summary>
/// Query for getting all paramedics.
/// </summary>
/// <param name="IsWorking">specifies whether to filter employees by their working status</param>
public record GetAllParamedicsQuery(bool? IsWorking = null) : IRequest<ErrorOr<GetAllParamedicsResponse>>;