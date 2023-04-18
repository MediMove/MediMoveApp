using MediMove.Server.Entities;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IAvailabilityRepository
    {
        Task<IEnumerable<Availability>> GetAvailabilities();
        Task<Availability> GetAvailability(int id);
    }
}
