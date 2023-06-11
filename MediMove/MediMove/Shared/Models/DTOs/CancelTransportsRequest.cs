namespace MediMove.Shared.Models.DTOs;

public record CancelTransportsRequest(
    int[] TransportIds);
