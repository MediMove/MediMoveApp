
namespace MediMove.Shared.Validators;

public static class PatientValidators
{
    public static bool IsValidWeight(this int value) =>
        value > 0 && value < 700;
}
