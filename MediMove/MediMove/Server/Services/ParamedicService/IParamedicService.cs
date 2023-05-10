using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Services.ParamedicService
{
    public interface IParamedicService
    {
        Task<ParamedicDTO> GetById(int id);
        Task<IEnumerable<ParamedicDTO>> GetAll();
        Task Create(CreateParamedicDTO dto);
    }
}