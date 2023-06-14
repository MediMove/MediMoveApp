using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs
{
    public class CreateAvailabilitiesRequest
    {
        public Dictionary<DateTime, ShiftType?> Availabilities { get; set; }
    }
}
