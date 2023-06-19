
namespace MediMove.Shared.Validators;

public static class SalaryValidators
{
    public static bool IsValidSalary(this decimal value) =>
        value > 0 && value < 20000;
}