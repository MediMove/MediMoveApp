
namespace MediMove.Shared.Models.DTOs
{
    /// <summary>
    /// Resppnse of getting available paramedics by date and shift.
    /// </summary>
    public record GetAvailableParamedicsByDateAndShiftResponse
    {
        public Dictionary<int, ParamedicInfo> Paramedics { get; set; }
        public record ParamedicInfo(
            string FirstName,
            string LastName,
            string PhoneNumber,
            bool IsDriver);
    }
}