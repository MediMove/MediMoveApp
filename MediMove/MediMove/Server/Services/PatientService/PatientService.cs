using AutoMapper;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Server.Exceptions;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPersonalInformationRepository _personalInformationRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper, IPersonalInformationRepository personalInformationRepository)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
            _personalInformationRepository = personalInformationRepository;
        }

        public async Task<IEnumerable<PatientNameDTO>> GetAll()
        {
            var patients = await _patientRepository.GetPatients() ?? throw new NotFoundException($"No patients found.");
            foreach (var patient in patients)
            {
                patient.PersonalInformation =
                    await _personalInformationRepository
                        .GetPersonalInformation(patient.PersonalInformationId);
            }

            var patientsNameDTO = _mapper.Map<IEnumerable<PatientNameDTO>>(patients);

            return patientsNameDTO; 
        }

        public async Task<PatientDTO> GetById(int id)
        {
            var patient = await _patientRepository.GetPatient(id) ?? throw new NotFoundException($"No patients found.");
            patient.PersonalInformation =
                await _personalInformationRepository
                    .GetPersonalInformation(patient.PersonalInformationId);

            var patientDTO = _mapper.Map<PatientDTO>(patient);

            return patientDTO;
        }

        public async Task<int> Create(CreatePatientDTO dto)
        {
            var newPatient = _mapper.Map<Patient>(dto);

            var id = await _patientRepository.Create(newPatient);

            return id;

        }

        public async Task Edit(int id, CreatePatientDTO dto)
        {
            throw new NotImplementedException();

            var patient = await _patientRepository.GetPatient(id);

            if (patient is null)
                throw new NotFoundException("Patient not found.");

            var updatedPatient = _mapper.Map<Patient>(dto);

            updatedPatient.Id = patient.Id;

            await _patientRepository.Update(updatedPatient);



        }
    }
}
