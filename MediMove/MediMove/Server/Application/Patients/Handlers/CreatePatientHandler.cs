using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Patients.Commands;
using MediMove.Server.Data;
using MediMove.Server.Errors;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs.V2;

namespace MediMove.Server.Application.Patients.Handlers
{
    public class CreatePatientHandler : IRequestHandler<CreatePatientCommand, ErrorOr<int>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public CreatePatientHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ErrorOr<int>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = _mapper.Map<Patient>(request.Dto);

            if (patient is null)
                return Errors.Errors.MappingError;

            // TODO: Add validation

            await _dbContext.Patients.AddAsync(patient, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return patient.Id;
        }
    }
}

