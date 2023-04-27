using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Services.DispatcherService
{
    public interface IDispatcherService
    {
        Task<DispatcherDTO> GetById(int id);
        Task<IEnumerable<DispatcherDTO>> GetAll();
        Task Create(CreateDispatcherDTO dto);
    }
}