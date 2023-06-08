using ErrorOr;
using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static ErrorOr<ShiftType> ToShiftType(this DateTime dateTime)
        {
            if (dateTime.Hour >= 6 && dateTime.Hour < 14)
                return ShiftType.Morning;
            else if (dateTime.Hour >= 14 && dateTime.Hour < 22)
                return ShiftType.Evening;
            else return Error.Failure("FAILURE", "DateTime is not in a valid shift");
        }
    }
}
