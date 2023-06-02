using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Patients.Queries;
using MediMove.Server.Data;
using MediMove.Server.Errors;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Patients.Handlers
{
    public class GetPatientHandler : IRequestHandler<GetPatientQuery, ErrorOr<PatientDTO>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetPatientHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ErrorOr<PatientDTO>> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            var patient = await _dbContext.Patients
                .Include(p => p.PersonalInformation)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
                //.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken); //Głupota

            if (patient is null)
                return Errors.Errors.EntityNotFoundError;

            var patientDTO = _mapper.Map<PatientDTO>(patient);

            if (patientDTO is null)
                return Errors.Errors.MappingError;

            return patientDTO;
        }
    }
}
