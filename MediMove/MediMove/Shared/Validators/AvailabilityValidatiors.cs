using MediMove.Shared.Extensions;
using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Validators
{
    public static class AvailabilityValidatiors
    {
        public static bool IsAvailabilityWithinFutureRange(DateTime date) =>
            date.Date > DateTime.Today.Date && date.Date <= DateTime.Today.AddYears(1);

        public static bool IsTodayBeforeShift(DateTime date, ShiftType? shift) =>
            date.Date == DateTime.Today && (shift ?? ShiftType.Morning).StartTime() > DateTime.Now.TimeOfDay;

        public static bool CanParamedicModifyAvailability(DateTime date, ShiftType? shift) =>
            IsAvailabilityWithinFutureRange(date) || IsTodayBeforeShift(date, shift);
    }
}