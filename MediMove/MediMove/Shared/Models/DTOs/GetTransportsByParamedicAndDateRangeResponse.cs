namespace MediMove.Shared.Models.DTOs;

public record GetTransportsByParamedicAndDateRangeResponse(
    ILookup<DateTime, GetTransportResponse> Transports);