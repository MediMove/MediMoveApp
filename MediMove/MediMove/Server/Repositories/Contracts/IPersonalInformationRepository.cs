using MediMove.Server.Models;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IPersonalInformationRepository
    {
        Task<IEnumerable<PersonalInformation>> GetPersonalInformations();
        Task<PersonalInformation> GetPersonalInformation(int id);
    }
}
