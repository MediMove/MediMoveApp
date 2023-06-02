using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Patients.Commands;
using MediMove.Server.Data;
using MediMove.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Patients.Handlers
{
    public class UpdatePatientHandler : IRequestHandler<UpdatePatientCommand, ErrorOr<Patient>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public UpdatePatientHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ErrorOr<Patient>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (patient is null)
                return Errors.Errors.EntityNotFoundError;
            
            patient.Weight = request.Dto.Weight;
            patient.PersonalInformation.FirstName = request.Dto.FirstName;
            patient.PersonalInformation.LastName = request.Dto.LastName;
            patient.PersonalInformation.StreetAddress = request.Dto.StreetAddress;
            patient.PersonalInformation.HouseNumber = request.Dto.HouseNumber;
            patient.PersonalInformation.ApartmentNumber = request.Dto.ApartmentNumber;
            patient.PersonalInformation.PostalCode = request.Dto.PostalCode;
            patient.PersonalInformation.StateProvince = request.Dto.StateProvince;
            patient.PersonalInformation.City = request.Dto.City;
            patient.PersonalInformation.Country = request.Dto.Country;
            patient.PersonalInformation.PhoneNumber = request.Dto.PhoneNumber;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return patient;
        }
    }
}
