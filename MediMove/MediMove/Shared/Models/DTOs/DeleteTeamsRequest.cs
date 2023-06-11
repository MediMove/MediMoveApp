
namespace MediMove.Shared.Models.DTOs
{
    /// <summary>
    /// Request of deleting teams.
    /// </summary>
    public record DeleteTeamsRequest(
        int[] TeamIds);
}