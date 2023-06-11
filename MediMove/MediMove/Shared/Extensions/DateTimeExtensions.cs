using ErrorOr;
using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static ErrorOr<ShiftType> ToShiftType(this DateTime dateTime)
        {
            var time = dateTime.TimeOfDay;
            if (time >= ShiftType.Morning.StartTime() && time < ShiftType.Morning.EndTime())
                return ShiftType.Morning;
            else if (time >= ShiftType.Evening.StartTime() && time < ShiftType.Evening.EndTime())
                return ShiftType.Evening;
            else return Error.Failure("DATETIME_TO_SHIFTTYPE_CONVERSION_FAILURE",
                "DateTime is not in a valid shift");
        }
    }
}
