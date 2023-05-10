using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Repositories.Contracts;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Queries.GetAllPatientsQuery;

public record GetAllPatientsDTO : IRequest<ErrorOr<IEnumerable<PatientDTO>>>;
public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsDTO, ErrorOr<IEnumerable<PatientDTO>>>
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;

    public GetAllPatientsQueryHandler(IMapper mapper, IPatientRepository patientRepository)
    {
        _mapper = mapper;
        _patientRepository = patientRepository;
    }

    public async Task<ErrorOr<IEnumerable<PatientDTO>>> Handle(GetAllPatientsDTO request, CancellationToken cancellationToken)
    {
        var patients = await _patientRepository.GetPatients();
        var patientDTOs = _mapper.Map<IEnumerable<PatientDTO>>(patients);

        if (patientDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(patientDTOs);
    }
}
