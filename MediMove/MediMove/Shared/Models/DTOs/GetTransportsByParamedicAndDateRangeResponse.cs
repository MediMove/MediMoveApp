namespace MediMove.Shared.Models.DTOs;

public record GetTransportsByParamedicAndDateRangeResponse(
    ILookup<DateTime, TransportDTO> Transports);