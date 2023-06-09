using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Extensions
{
    public static class ShiftTypeExtensions
    {
        public static TimeSpan StartTime(this ShiftType shift)
        {
            return shift switch
            {
                ShiftType.Morning => new TimeSpan(6, 0, 0),
                ShiftType.Evening => new TimeSpan(14, 0, 0),
                _ => throw new ArgumentException("Invalid ShiftType value.")
            };
        }

        public static TimeSpan EndTime(this ShiftType shift)
        {
            return shift switch
            {
                ShiftType.Morning => ShiftType.Evening.StartTime(),
                ShiftType.Evening => new TimeSpan(22, 0, 0),
                _ => throw new ArgumentException("Invalid ShiftType value.")
            };
        }
    }
}