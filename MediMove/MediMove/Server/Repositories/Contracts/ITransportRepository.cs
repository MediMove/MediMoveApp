using MediMove.Shared.Entities;

namespace MediMove.Server.Repositories.Contracts
{
    public interface ITransportRepository
    {
        Task<IEnumerable<Transport>> GetTransports();
        Task<Transport> GetTransport(int id);
    }
}