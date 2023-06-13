
namespace MediMove.Shared.Models.DTOs
{
    /// <summary>
    /// Request of deleting availabilities.
    /// </summary>
    /// <param name="AvailabilityDates"></param>
    public record DeleteAvailabilitiesRequest(
        DateTime[] AvailabilityDates);
}
