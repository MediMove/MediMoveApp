using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Queries.GetPatientQuery;

public record GetPatientDTO(int Id) : IRequest<ErrorOr<PatientDTO>>;
public class GetPatientQueryHandler : IRequestHandler<GetPatientDTO, ErrorOr<PatientDTO>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetPatientQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<PatientDTO>> Handle(GetPatientDTO request, CancellationToken cancellationToken)
    {
        var patient = await _dbContext.Patients.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (patient is null)
            return Errors.Errors.EntityNotFoundError;

        var patientDTO = _mapper.Map<PatientDTO>(patient);

        if (patientDTO is null)
            return Errors.Errors.MappingError;

        return patientDTO;
    }
}
