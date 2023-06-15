
namespace MediMove.Shared.Models.DTOs;

/// <summary>
/// Response of getting teams by date and shift.
/// </summary>
public record GetTeamsByDateAndShiftResponse
{
    public Dictionary<int, TeamInfo> Teams { get; set; }
    public record TeamInfo(
        int DriverId,
        string DriverFirstName,
        string DriverLastName,
        string DriverPhoneNumber,

        int ParamedicId,
        string ParamedicFirstName,
        string ParamedicLastName,
        string ParamedicPhoneNumber);
}