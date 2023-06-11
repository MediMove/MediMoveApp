
namespace MediMove.Shared.Models.DTOs
{
    /// <summary>
    /// Response of getting teams by day and shift.
    /// </summary>
    public class GetTeamsByDayAndShiftResponse
    {
        public Dictionary<int, TeamInfo> Teams { get; set; }
        public class TeamInfo
        {
            public int DriverId { get; set; }
            public string DriverFirstName { get; set; }
            public string DriverLastName { get; set; }
            public string DriverPhoneNumber { get; set; }

            public int ParamedicId { get; set; }
            public string ParamedicFirstName { get; set; }
            public string ParamedicLastName { get; set; }
            public string ParamedicPhoneNumber { get; set; }
        }
    }
}