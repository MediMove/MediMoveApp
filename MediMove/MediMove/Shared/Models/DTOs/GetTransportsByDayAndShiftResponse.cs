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
        string PatientFirstName,
        string PatientLastName,
        string PatientPhoneNumber,
        DateTime StartTime,
        string Destination,
        TransportType TransportType,
        string? StartLocation,
        string? ReturnLocation,
        string? Note
        );
}