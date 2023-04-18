using MediMove.Shared.Entities;

namespace MediMove.Server.Services.TransportService
{
    public interface ITransportService
    {
        Task<List<Transport>> GetByDay(DateTime date);
        Task<List<Transport>> GetByParamedicId(int id, DateTime date);
    }
}