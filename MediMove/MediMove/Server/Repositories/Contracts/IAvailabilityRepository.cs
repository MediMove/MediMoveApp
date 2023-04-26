using MediMove.Server.Models;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IAvailabilityRepository
    {
        Task<IEnumerable<Availability>> GetAvailabilities();
        Task<Availability> GetAvailability(int id);
    }
}
