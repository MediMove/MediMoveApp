using MediMove.Shared.Entities;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IBillingRepository
    {
        Task<IEnumerable<Billing>> GetBillings();
        Task<Billing> GetBilling(int id);
    }
}
