using MediMove.Server.Data;
using MediMove.Server.Entities;
using MediMove.Server.Exceptions;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Services.TransportService
{
    public class TransportService : ITransportService
    {
        private readonly ITransportRepository _transportRepository;

        public TransportService(ITransportRepository transportRepository)
        {
            _transportRepository = transportRepository;
        }

        public async Task<List<Transport>> GetByParamedicId(int id, DateTime date)
        {

            var transports = await _transportRepository.GetTransportsForParamedic(id,date);

            if (transports is null)
                throw new NotFoundException($"Transports with id :{id} and date: {date}, were not found.");

            return transports;

            /*
             * Pod dodaniu TransportDTO
             * Utworzyć maper z Transport na TransportDTO, 
             * możliwe że wtedy trzeba będzie użyć include przy wyciąganiu z bazy danych po to aby dodać do transportu informacje z innych tabel.
             * Zwrócić transportDTO zamiast Transport.
             */

        }

        public async Task<List<Transport>> GetByDay(DateTime date)
        {
            var transports = await _transportRepository.GetTransportsForDay(date);

            if (transports is null)
                throw new NotFoundException($"Transports with date: {date}, were not found."); 

            //Dodać mapper jak wyżej w komentarzu

            return transports;

        }

        public async Task<List<Transport>> GetAll()
        {
            var transports = await _transportRepository.GetTransports();
            if (transports is null)
                throw new NotFoundException($"No transports found.");

            return transports;
        }

    }
}
