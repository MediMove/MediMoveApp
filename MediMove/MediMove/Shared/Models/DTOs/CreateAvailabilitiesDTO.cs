using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs
{
    public class CreateAvailabilitiesDTO
    {
        public IEnumerable<(DateTime Day, ShiftType Shift)> Availabilities { get; set; }
    }
}
