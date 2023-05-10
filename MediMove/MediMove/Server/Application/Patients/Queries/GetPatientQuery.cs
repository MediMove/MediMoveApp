using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Queries.GetPatientQuery;

public record GetPatientDTO(int Id) : IRequest<ErrorOr<PatientDTO>>;
public class GetPatientQueryHandler : IRequestHandler<GetPatientDTO, ErrorOr<PatientDTO>>
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;

    public GetPatientQueryHandler(IMapper mapper, IPatientRepository patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
    }

    public async Task<ErrorOr<PatientDTO>> Handle(GetPatientDTO request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetPatient(request.Id);

        if (patient is null)
            return Errors.Errors.EntityNotFoundError;

        var patientDTO = _mapper.Map<PatientDTO>(patient);

        if (patientDTO is null)
            return Errors.Errors.MappingError;

        return patientDTO;
    }
}
