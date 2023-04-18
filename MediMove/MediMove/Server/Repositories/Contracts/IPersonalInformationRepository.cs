using MediMove.Server.Entities;

namespace MediMove.Server.Repositories.Contracts
{
    public interface IPersonalInformationRepository
    {
        Task<IEnumerable<PersonalInformation>> GetPersonalInformations();
        Task<PersonalInformation> GetPersonalInformation(int id);
    }
}
