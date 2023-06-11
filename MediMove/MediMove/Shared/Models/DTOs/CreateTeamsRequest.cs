using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs
{
    public class CreateTeamsRequest
    {
        public DateTime Day { get; set; }
        public ShiftType Shift { get; set; }
        public IEnumerable<TeamInfo> Teams { get; set; }
        public record TeamInfo(int DriverId, int ParamedicId);
    }
}
