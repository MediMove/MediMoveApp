using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Models.DTOs
{
    public record CreateTeamsRequest
    {
        public DateTime Date { get; set; }
        public ShiftType Shift { get; set; }
        public HashSet<TeamInfo> Teams { get; set; }
        public record TeamInfo(int DriverId, int ParamedicId);
    }
}
