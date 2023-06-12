using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Server.Application.Patients.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Patients.Handlers
{
    public class GetAllPatientsHandler : IRequestHandler<GetAllPatientsQuery, ErrorOr<GetAllPatientsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetAllPatientsHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ErrorOr<GetAllPatientsResponse>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _dbContext.Patients
                .Include(p => p.PersonalInformation)
                .Select(p => new
                {
                    p.Id,
                    GetAllPatientsResponse = _mapper.Map<PatientDTO>(p)
        })
                .ToDictionaryAsync(
                    keySelector: t => t.Id,
                    elementSelector: t => t.GetAllPatientsResponse, cancellationToken
            );



            if (patients is null)
                return Errors.Errors.MappingError;

            return new GetAllPatientsResponse(patients);
        }
    }
}
