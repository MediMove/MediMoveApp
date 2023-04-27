using MediMove.Shared.Models.DTOs.temp;
using MediMove.Shared.Models.Enums;

namespace MediMove.Server.Services.AvailabilityService
{
    public interface IAvailabilityService
    {
        Task<AvailabilityDTO> GetById(int id);
        Task<IEnumerable<AvailabilityDTO>> GetByParamedic(int id);
        Task BulkCreate(int id, IEnumerable<ShiftType> dto);
    }
}