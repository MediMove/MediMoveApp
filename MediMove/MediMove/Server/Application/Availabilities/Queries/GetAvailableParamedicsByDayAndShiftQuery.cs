using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Application.Availabilities.Queries;

public record GetAvailableParamedicsByDayAndShiftQuery(DateTime Day, ShiftType Shift) : IRequest<ErrorOr<GetAvailableParamedicsByDayAndShiftResponse>>;

