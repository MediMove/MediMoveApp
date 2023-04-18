using MediMove.Shared.Entities;

namespace MediMove.Server.Repositories.Contracts
{
    public interface ITransportRepository
    {
        Task<List<Transport>> GetTransportsForParamedic(int id, DateTime date);
        Task<List<Transport>> GetTransportsForDay(DateTime date);
    }
}