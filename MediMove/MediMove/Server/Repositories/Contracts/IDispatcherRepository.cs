using MediMove.Server.Models;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IDispatcherRepository
    {
        Task<IEnumerable<Dispatcher>> GetDispatchers();
        Task<Dispatcher> GetDispatcher(int id);
        Task Create(Dispatcher d);
    }
}
