using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Handlers
{
    /// <summary>
    /// Handler for getting transports by paramedic and date range.
    /// </summary>
    public class GetTransportsByParamedicAndDateRangeQueryHandler : IRequestHandler<GetTransportsByParamedicAndDateRangeQuery, ErrorOr<GetTransportsByParamedicAndDateRangeResponse>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetTransportsByParamedicAndDateRangeQueryHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public GetTransportsByParamedicAndDateRangeQueryHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetTransportsByParamedicAndDateRangeQuery.
        /// </summary>
        /// <param name="query">GetTransportsByParamedicAndDateRangeQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetTransportsByParamedicAndDateRangeResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetTransportsByParamedicAndDateRangeResponse>> Handle(GetTransportsByParamedicAndDateRangeQuery query, CancellationToken cancellationToken)
        {
            var transports = await _dbContext.Transports
                .Where(t => !t.IsCancelled &&
                    (!query.StartDateInclusive.HasValue || t.StartTime.Date >= query.StartDateInclusive.Value.Date) &&
                    (!query.EndDateInclusive.HasValue || t.StartTime.Date <= query.EndDateInclusive.Value.Date) &&
                    (t.Team.DriverId == query.ParamedicId || t.Team.ParamedicId == query.ParamedicId))
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .Select(t => new GetTransportResponse()
                {
                    PatientFirstName = t.Patient.PersonalInformation.FirstName,
                    PatientLastName = t.Patient.PersonalInformation.LastName,
                    PatientPhoneNumber = t.Patient.PersonalInformation.PhoneNumber,
                    PatientStreetAddress = t.Patient.PersonalInformation.StreetAddress,
                    PatientHouseNumber = t.Patient.PersonalInformation.HouseNumber,
                    PatientApartmentNumber = t.Patient.PersonalInformation.ApartmentNumber,
                    PatientPostalCode = t.Patient.PersonalInformation.PostalCode,
                    PatientCity = t.Patient.PersonalInformation.City,
                    PatientWeight = t.Patient.Weight,
                    StartTime = t.StartTime,
                    Financing = t.Financing,
                    PatientPosition = t.PatientPosition,
                    Destination = t.Destination,
                    TransportType = t.TransportType
                })
                .ToArrayAsync(cancellationToken);

            return new GetTransportsByParamedicAndDateRangeResponse(
                transports.ToLookup(t => t.StartTime.Date));
        }
    }
}