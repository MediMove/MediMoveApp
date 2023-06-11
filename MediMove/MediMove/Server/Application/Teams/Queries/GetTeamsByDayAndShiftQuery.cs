using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Application.Teams.Queries;
/// <summary>
/// Query for getting teams by day and shift.
/// </summary>
/// <param name="Day">day as DateTime</param>
/// <param name="Shift">shift as ShiftType</param>
public record GetTeamsByDayAndShiftQuery(DateTime Day, ShiftType Shift) : IRequest<ErrorOr<GetTeamsByDayAndShiftResponse>>;
