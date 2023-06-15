using MediMove.Shared.Extensions;
using MediMove.Shared.Models.Enums;

namespace MediMove.Shared.Validators
{
    public static class AvailabilityValidatiors
    {
        public static bool IsValidFutureDate(DateTime date) =>
            date.Date > DateTime.Today && date.Date <= DateTime.Today.AddYears(1);

        public static bool IsBeforeShift(ShiftType? shift) =>
            (shift ?? ShiftType.Morning).StartTime() > DateTime.Now.TimeOfDay;

        public static bool CanExecuteCommands(DateTime date, ShiftType? shift) =>
            IsValidFutureDate(date) || (date.Date == DateTime.Today && IsBeforeShift(shift));
    }
}