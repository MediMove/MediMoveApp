using AutoMapper;
using MediMove.Server.Data;
using MediMove.Server.Entities;
using MediMove.Server.Exceptions;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Entities;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Services.TransportService
{
    public class TransportService : ITransportService
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IMapper _mapper;

        public TransportService(ITransportRepository transportRepository, IMapper mapper)
        {
            _transportRepository = transportRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransportDTO>> GetByParamedicId(int id, DateOnly date)
        {

            var transports = await _transportRepository.GetTransportsForParamedic(id,date);

            if (transports is null)
                throw new NotFoundException($"Transports with id :{id} and date: {date}, were not found.");

            var transportsDTO = _mapper.Map<IEnumerable<TransportDTO>>(transports);

            return transportsDTO;


        }

        public async Task<IEnumerable<TransportDTO>> GetByDay(DateOnly date)
        {
            var transports = await _transportRepository.GetTransportsForDay(date);

            if (transports is null)
                throw new NotFoundException($"Transports with date: {date}, were not found.");

            var transportsDTO = _mapper.Map<IEnumerable<TransportDTO>>(transports);

            return transportsDTO;

        }

        public async Task<IEnumerable<TransportDTO>> GetAll()
        {
            var transports = await _transportRepository.GetTransports();
            if (transports is null)
                throw new NotFoundException($"No transports found.");

            var transportsDTO = _mapper.Map<IEnumerable<TransportDTO>>(transports);

            return transportsDTO;
        }

        public async Task Create(CreateTransportDTO dto)
        {
            var newTransport = _mapper.Map<Transport>(dto);
            
            await _transportRepository.Create(newTransport);

        }
        public async Task Edit(CreateTransportDTO dto, int id)
        {
            throw new NotImplementedException(); // Ta logika chyba musi być w repository, ogólnie nw, ja w projekcie to miałem w service bo nie używałem repository

            var transport = await _transportRepository.GetTransport(id);

            if (transport is null)
                throw new NotFoundException("Transport not found.");

            var updatedTransport = _mapper.Map<Transport>(dto);

            updatedTransport.Id = transport.Id;

            await _transportRepository.Update(updatedTransport);

        }

    }
}
