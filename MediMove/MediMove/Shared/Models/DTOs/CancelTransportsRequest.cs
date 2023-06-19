namespace MediMove.Shared.Models.DTOs;

public record CancelTransportsRequest(
    HashSet<int> TransportIds);
