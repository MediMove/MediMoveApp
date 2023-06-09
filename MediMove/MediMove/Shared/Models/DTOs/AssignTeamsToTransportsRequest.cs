
namespace MediMove.Shared.Models.DTOs
{
    /// <summary>
    /// Request of assigning teams to transports.
    /// </summary>
    public record AssignTeamsToTransportsRequest
    {
        /// <summary>
        /// Keys are transport IDs, values are team IDs.
        /// </summary>
        public SortedDictionary<int, int> TransportsToTeams { get; set; }
    }
}
