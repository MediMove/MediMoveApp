
namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// Request of deleting availabilities.
/// </summary>
/// <param name="AvailabilityDates">dates of availabilities to delete</param>
public record DeleteAvailabilitiesRequest(
    HashSet<DateTime> AvailabilityDates);