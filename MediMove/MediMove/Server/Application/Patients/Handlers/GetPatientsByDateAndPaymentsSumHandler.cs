using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Patients.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Patients.Handlers


{
   
    public class GetPatientsByDateAndPaymentsSumHandler : IRequestHandler<GetPatientsByDateAndPaymentsSumQuery, ErrorOr<GetPatientsByDateAndPaymentsSumDTO>>
    {
        private TProperty GetValueOrDefault<TModel, TProperty>(TModel model, string propertyName)
        {
            var propertyInfo = typeof(TModel).GetProperty(propertyName);
            if (propertyInfo != null && propertyInfo.PropertyType == typeof(TProperty))
            {
                return (TProperty)propertyInfo.GetValue(model);
            }

            return default(TProperty);
        }
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        public GetPatientsByDateAndPaymentsSumHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<GetPatientsByDateAndPaymentsSumDTO>> Handle(GetPatientsByDateAndPaymentsSumQuery request, CancellationToken cancellationToken)
        {
            DateTime StartDate = DateTime.UtcNow.AddDays(-10);
            DateTime EndDate = DateTime.UtcNow.AddDays(10);
            var patients = await _dbContext.Transports
            //.Include(t => t.Billing)
            .Include(t => t.Patient)
            //,JoinThenInclude(p => p.PersonalInformation)

            .Join(_dbContext.Billings,
                transport => transport.BillingId,
                billing => billing.Id,
                (transport, billing) => new { Id = transport.PatientId, Weight = transport.Patient.Weight, PersonalInfoID = transport.Patient.PersonalInformationId, Transport = transport, Billing = billing })
            .Where(joinResult => joinResult.Billing.InvoiceDate.Date >= request.StartDate.Date &&
                                 joinResult.Billing.InvoiceDate.Date <= request.EndDate.Date)
            .Join(_dbContext.PersonalInformations,
                patient => patient.Transport.Patient.PersonalInformationId,
                personalinfo => personalinfo.Id,
                (patient, personalinfo) => new //{ Transport = transport.Transport, PersonalInformation = personalinfo })
                {
                    Id = patient.Id,
                    Weight = patient.Weight,
                    PerosnalInfo = personalinfo,
                    Transport = patient.Transport,
                    Billing = patient.Billing,
                })

                
            .GroupBy(joinResult => joinResult.PerosnalInfo)
            .Select(patient => new GetPatientsByDateAndPaymentsSumDTO.GetPatientsByDateAndPaymentsSumRow
            {
                Id = patient.Key.Id,
                Weight = patient.FirstOrDefault().Weight,
                FirstName = patient.Key.FirstName,
                LastName = patient.Key.LastName,
                StreetAddress = patient.Key.StreetAddress,
                HouseNumber = patient.Key.HouseNumber,
                ApartmentNumber = patient.Key.ApartmentNumber,
                City = patient.Key.City,
                PostalCode = patient.Key.PostalCode,
                StateProvince = patient.Key.StateProvince,
                Country = patient.Key.Country,
                PhoneNumber = patient.Key.PhoneNumber,
                PaymentsSum = patient.Sum(t => t.Billing.Cost)
            })
            .Where(dto => dto.PaymentsSum >= request.StartAmount &&
                          dto.PaymentsSum <= request.EndAmount)
            .ToListAsync(cancellationToken);

            //var filteredPatients;
            //foreach (var patient in patients)
            //{
            //    filteredPatients += patient.Sum(p => p.Billing.Cost).To
            //}
            //DateTime InvoiceDate = DateTime.UtcNow;

            //var temp = InvoiceDate.Date >= StartDate.Date && InvoiceDate.Date <= EndDate.Date;
            var result = new GetPatientsByDateAndPaymentsSumDTO
            {
                Rows = patients
            };

            var getPatientsByDateAndPaymentsSumDTOs = _mapper.Map<GetPatientsByDateAndPaymentsSumDTO>(result);


            //if (getPatientsByDateAndPaymentsSumDTOs is null)
            //    return Errors.Errors.MappingError;

            return ErrorOr.ErrorOr.From(getPatientsByDateAndPaymentsSumDTOs);

            //return ErrorOr<GetPatientsByDateAndPaymentsSumDTO>.Success(result);
            //var paramedics = await _dbContext.Paramedics
            //     .Include(e => e.Transports)
            //     .Include(e => e.Rates)
            //     .ToListAsync(cancellationToken);
            //throw new NotImplementedException();
        }
    }
}