using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Application.Teams.Queries;
/// <summary>
/// Query for getting teams by date and shift.
/// </summary>
/// <param name="Date">date as DateTime</param>
/// <param name="Shift">shift as ShiftType</param>
public record GetTeamsByDateAndShiftQuery(DateTime Date, ShiftType Shift) : IRequest<ErrorOr<GetTeamsByDateAndShiftResponse>>;
