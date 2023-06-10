
namespace MediMove.Shared.Models.DTOs
{
    /// <summary>
    /// Request of deleting teams.
    /// </summary>
    public record DeleteTeamsRequest
    {
        public int[] TeamIds { get; set; }
    }
}