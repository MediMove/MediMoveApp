
namespace MediMove.Shared.Models.DTOs
{
    public class GetAvailableParamedicsByDayAndShiftResponse
    {
        public Dictionary<int, ParamedicInfo> Paramedics { get; set; }
        public record ParamedicInfo(string FirstName, string LastName, bool IsDriver);
    }
}
