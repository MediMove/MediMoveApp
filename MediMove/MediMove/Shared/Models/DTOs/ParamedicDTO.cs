
namespace MediMove.Shared.Models.DTOs;

public record ParamedicDTO : EmployeeDTO
{
    public bool IsDriver { get; set; }
    public decimal PayPerHour { get; set; }
}
