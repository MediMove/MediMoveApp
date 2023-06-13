using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs
{
    /// <summary>
    /// Response for getting availabilities for paramedic by date range.
    /// </summary>
    public record GetAvailabilitiesForParamedicByDateRangeResponse
    {
        public Dictionary<DateTime, DayInfo> DayToDayInfo { get; set; }
        public record DayInfo(ShiftType? DeclaredShift, ShiftType? AssignedTeamShift); // DeclaredShift = shift declared by paramedic, AssignedTeamShift = shift assigned to a team on this day. If null, paramedic does not have a team assigned on this day and can delete availability
    }
}