using MediMove.Shared.Entities;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IParamedicRepository
    {
        Task<IEnumerable<Paramedic>> GetParamedics();
        Task<Paramedic> GetParamedic(int id);
    }
}
