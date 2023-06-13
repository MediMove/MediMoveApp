using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Application.Transports.Queries;

/// <summary>
/// Query for getting transports by day and shift.
/// </summary>
/// <param name="Day"></param>
/// <param name="Shift"></param>
public record GetTransportsByDayAndShiftQuery(DateTime Day, ShiftType Shift) : IRequest<ErrorOr<GetTransportsByDayAndShiftResponse>>;