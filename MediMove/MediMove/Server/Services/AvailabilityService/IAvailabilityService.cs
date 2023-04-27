using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Services.AvailabilityService
{
    public interface IAvailabilityService
    {
        Task<AvailabilityDTO> GetById(int id);
        Task<IEnumerable<AvailabilityDTO>> GetByParamedic(int id);
        Task BulkCreate(int id, IEnumerable<AvailabilityDTO> dto);
    }
}