using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Models;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Commands.CreatePatientCommand;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientDTO, ErrorOr<int>>
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;

    public CreatePatientCommandHandler(IMapper mapper, IPatientRepository patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
    }

    public async Task<ErrorOr<int>> Handle(CreatePatientDTO request, CancellationToken cancellationToken)
    {
        var patient = _mapper.Map<Patient>(request);

        if (patient is null)
            return Errors.Errors.MappingError;

        throw new NotImplementedException("Validation not implemented");
        // TODO: Add patient to database

        return patient.Id;
    }
}
