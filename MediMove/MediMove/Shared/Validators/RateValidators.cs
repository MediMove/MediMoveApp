
namespace MediMove.Shared.Validators;

public static class RateValidators
{
    public static bool IsValidPayPerHour(this decimal value) =>
        value > 0 && value < 100;
}
