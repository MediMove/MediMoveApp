using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// Request for creating availabilities.
/// </summary>
public record CreateAvailabilitiesRequest
{
    public Dictionary<DateTime, ShiftType?> Availabilities { get; set; }
}
