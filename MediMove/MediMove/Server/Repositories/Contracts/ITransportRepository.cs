using MediMove.Server.Entities;


namespace MediMove.Server.Repositories.Contracts
{
    public interface ITransportRepository
    {
        Task<IEnumerable<Transport>> GetTransportsForParamedic(int id, DateOnly date);
        Task<IEnumerable<Transport>> GetTransportsForDay(DateOnly date);
        Task<IEnumerable<Transport>> GetTransports();
        Task<Transport> GetTransport(int id);
        Task Create(Transport dto);
        Task Update(Transport dto);

    }
}