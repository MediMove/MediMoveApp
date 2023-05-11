using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Data;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Patients.Commands.CreatePatientCommand;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientDTO, ErrorOr<int>>
{
    private readonly IMapper _mapper;
    private readonly MediMoveDbContext _dbContext;

    public CreatePatientCommandHandler(IMapper mapper, MediMoveDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<int>> Handle(CreatePatientDTO request, CancellationToken cancellationToken)
    {
        var patient = _mapper.Map<Patient>(request);

        if (patient is null)
            return Errors.Errors.MappingError;

        // TODO: Add validation

        await _dbContext.Patients.AddAsync(patient, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return patient.Id;
    }
}
