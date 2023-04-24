using MediMove.Server.Entities;


namespace MediMove.Server.Repositories.Contracts
{
    public interface ITransportRepository
    {
        Task<IEnumerable<Transport>> GetTransportsForParamedic(int id, DateTime date);
        Task<IEnumerable<Transport>> GetTransportsForDay(DateTime date);
        Task<IEnumerable<Transport>> GetTransports();
        Task<Transport> GetTransport(int id);
        Task Create(Transport dto);
        Task Update(Transport dto);

    }
}