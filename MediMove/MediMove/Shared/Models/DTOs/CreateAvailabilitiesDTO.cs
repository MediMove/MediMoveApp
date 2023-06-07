using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs
{
    public class CreateAvailabilitiesDTO
    {
        public IEnumerable<Declaration> Availabilities { get; set; }
        public record Declaration(DateTime Day, ShiftType Shift);
    }
}
