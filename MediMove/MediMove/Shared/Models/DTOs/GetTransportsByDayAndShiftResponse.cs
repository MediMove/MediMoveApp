namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// Response of getting transports by day and shift.
/// </summary>
/// <param name="Transports">dictionary of (Transport ID, TransportDTO) pairs</param>
public record GetTransportsByDayAndShiftResponse(Dictionary<int, GetTransportResponse> Transports);
