using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs;

public record GetTransportsByParamedicAndDateRangeResponse
{
    public record TransportInfo(
        string PatientFirstName,
        string PatientLastName,
        string PatientPhoneNumber,
        int PatientWeight,

        string StartLocation,
        string Destination,
        string? ReturnLocation,
        DateTime StartTime,
        Financing Financing,
        PatientPosition PatientPosition,
        string? Note);

    public Dictionary<DateTime, TransportInfo[]> Transports { get; set; }
};

