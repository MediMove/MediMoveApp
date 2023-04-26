﻿using AutoMapper;
using MediMove.Server.Data;
using MediMove.Server.Entities;
using MediMove.Server.Exceptions;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MediMove.Server.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientNameDTO>> GetAll()
        {
            var patients = await _patientRepository.GetPatients();

            if (patients is null)
                throw new NotFoundException($"No patients found.");

            var patientsNameDTO = _mapper.Map<IEnumerable<PatientNameDTO>>(patients);

            return patientsNameDTO; 
        }

        public async Task<PatientDTO> Get(int id)
        {
            var patient = await _patientRepository.GetPatient(id);

            if (patient is null)
                throw new NotFoundException($"No patients found.");

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