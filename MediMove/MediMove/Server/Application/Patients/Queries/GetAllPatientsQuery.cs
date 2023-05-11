using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Patients.Queries.GetAllPatientsQuery;

public record GetAllPatientsDTO : IRequest<ErrorOr<IEnumerable<PatientDTO>>>;
public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsDTO, ErrorOr<IEnumerable<PatientDTO>>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public GetAllPatientsQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<IEnumerable<PatientDTO>>> Handle(GetAllPatientsDTO request, CancellationToken cancellationToken)
    {
        var patients = await _dbContext.Patients.ToListAsync(cancellationToken: cancellationToken);

        var patientDTOs = _mapper.Map<IEnumerable<PatientDTO>>(patients);

        if (patientDTOs is null)
            return Errors.Errors.MappingError;

        return ErrorOr.ErrorOr.From(patientDTOs);
    }
}
