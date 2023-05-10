
using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateOnly ToDateOnly(this DateTime date)
        {
            return DateOnly.FromDateTime(date);
        }

        public static bool CompareToDateOnly(this DateTime dateTime, DateOnly dateOnly)
        {
            return DateOnly.FromDateTime(dateTime) == dateOnly;
        }

        public static ShiftType? ToShiftType(this DateTime dateTime)
        {
            //if (dateTime.Hour < 6 || dateTime.Hour >= 22)
            //    return null;

            return dateTime.Hour >= 6 && dateTime.Hour < 18 ? ShiftType.Morning : ShiftType.Evening;
        }
    }
}
