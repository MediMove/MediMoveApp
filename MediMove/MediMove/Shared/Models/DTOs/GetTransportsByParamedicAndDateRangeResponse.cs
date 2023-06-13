namespace MediMove.Shared.Models.DTOs;

public record GetTransportsByParamedicAndDateRangeResponse(
    Dictionary<DateTime,IEnumerable<TransportDTO>> Transports);