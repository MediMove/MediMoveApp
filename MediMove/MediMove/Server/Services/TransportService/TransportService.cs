using AutoMapper;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Exceptions;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Services.TransportService
{
    public class TransportService : ITransportService
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IPersonalInformationRepository _personalInformationRepository;
        private readonly IMapper _mapper;

        public TransportService(ITransportRepository transportRepository, IMapper mapper, IPatientRepository patientRepository, IPersonalInformationRepository personalInformationRepository)
        {
            _transportRepository = transportRepository;
            _mapper = mapper;
            _patientRepository = patientRepository;
            _personalInformationRepository = personalInformationRepository;
        }

        public async Task<IEnumerable<TransportDTO>> GetByParamedicAndDay(int id, DateOnly date)
        {

            var transports = await _transportRepository.GetByParamedicAndDay(id,date) ?? throw new NotFoundException($"Transports with id :{id} and date: {date}, were not found.");
            foreach (var transport in transports)       //tymaczasowo
            {
                transport.Patient = await _patientRepository.GetPatient(transport.PatientId);
                transport.Patient.PersonalInformation = await _personalInformationRepository.GetPersonalInformation(transport.PatientId);
            }

            var transportsDTO = _mapper.Map<IEnumerable<TransportDTO>>(transports);

            return transportsDTO;


        }

        public async Task<IEnumerable<TransportDTO>> GetByDay(DateOnly date)
        {
            var transports = await _transportRepository.GetTransportsForDay(date) ?? throw new NotFoundException($"Transports with date: {date}, were not found.");
            foreach (var transport in transports)       //tymaczasowo
            {
                transport.Patient = await _patientRepository.GetPatient(transport.PatientId);
                transport.Patient.PersonalInformation = await _personalInformationRepository.GetPersonalInformation(transport.PatientId);
            }

            var transportsDTO = _mapper.Map<IEnumerable<TransportDTO>>(transports);

            return transportsDTO;

        }

        public async Task<IEnumerable<TransportDTO>> GetAll()
        {
            var transports = await _transportRepository.GetTransports() ?? throw new NotFoundException($"No transports found.");
            foreach (var transport in transports)       //tymaczasowo
            {
                transport.Patient = await _patientRepository.GetPatient(transport.PatientId);
                transport.Patient.PersonalInformation = await _personalInformationRepository.GetPersonalInformation(transport.PatientId);
            }

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
