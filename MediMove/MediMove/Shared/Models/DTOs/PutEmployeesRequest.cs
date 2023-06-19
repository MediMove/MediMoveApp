
namespace MediMove.Shared.Models.DTOs;

public record PutEmployeesRequest(
    ParamedicDTO[] Paramedics,
    DispatcherDTO[] Dispatchers);