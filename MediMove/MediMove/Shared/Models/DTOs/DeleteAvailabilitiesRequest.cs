
namespace MediMove.Shared.Models.DTOs
{
    /// <summary>
    /// Request for deleting availabilities.
    /// </summary>
    /// <param name="AvailabilityDates"></param>
    public record DeleteAvailabilitiesRequest(
        IEnumerable<DateTime> AvailabilityDates);
}
