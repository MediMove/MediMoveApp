using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs
{
    /// <summary>
    /// Response for getting availabilities by paramedic.
    /// </summary>
    /// <param name="DayToShift">dictionary of day to shift</param>
    public record GetAvailabilitiesByParamedicResponse(Dictionary<DateTime, ShiftType?> DayToShift);
}