using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// Response of getting transports by day and shift.
/// </summary>
/// <param name="Transports">dictionary of Transport ID -> TransportInfo</param>
public record GetTransportsByDayAndShiftResponse {
    public Dictionary<int, TransportInfo> Transports { get; set; }

    public record TransportInfo(
        int? TeamId,

        string StartLocation,
        string Destination,
        string? ReturnLocation,
        DateTime StartTime,
        string? Note);
}