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
    public class GetAllPatientsHandler : IRequestHandler<GetAllPatientsQuery, ErrorOr<IEnumerable<PatientDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetAllPatientsHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ErrorOr<IEnumerable<PatientDTO>>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _dbContext.Patients
                .Include(p => p.PersonalInformation)
                .ToListAsync(cancellationToken: cancellationToken);

            var patientDTOs = _mapper.Map<IEnumerable<PatientDTO>>(patients);

            if (patientDTOs is null)
                return Errors.Errors.MappingError;

            return ErrorOr.ErrorOr.From(patientDTOs);
        }
    }
}
