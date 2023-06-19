using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Patients.Commands;
using MediMove.Server.Data;


namespace MediMove.Server.Application.Patients.Handlers
{
    public class CreatePatientHandler : IRequestHandler<CreatePatientCommand, ErrorOr<Patient>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public CreatePatientHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ErrorOr<Patient>> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
        {
            var patient = _mapper.Map<Patient>(command.Request);

            if (patient is null)
                return Errors.Errors.MappingError;

            await _dbContext.Patients.AddAsync(patient, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return patient;
        }
    }
}

