using MediMove.Server.Entities;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Services.TransportService
{
    public interface ITransportService
    {
        Task<IEnumerable<TransportDTO>> GetByDay(DateTime date);
        Task<IEnumerable<TransportDTO>> GetByParamedicId(int id, DateTime date);
        Task<IEnumerable<TransportDTO>> GetAll();
        Task Create(CreateTransportDTO dto);
        Task Edit(CreateTransportDTO dto, int id);
        
    }
}