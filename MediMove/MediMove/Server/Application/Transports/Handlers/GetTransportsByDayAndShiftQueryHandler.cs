using ErrorOr;
using MediatR;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Transports.Handlers
{
    /// <summary>
    /// Handler for getting transports by day and shift.
    /// </summary>
    public class GetTransportsByDayAndShiftQueryHandler : IRequestHandler<GetTransportsByDayAndShiftQuery, ErrorOr<GetTransportsByDayAndShiftResponse>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetTransportsByDayAndShiftQueryHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public GetTransportsByDayAndShiftQueryHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetTransportsByDayAndShiftQuery.
        /// </summary>
        /// <param name="request">GetTransportsByDayAndShiftQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetTransportsByDayAndShiftResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetTransportsByDayAndShiftResponse>> Handle(GetTransportsByDayAndShiftQuery request, CancellationToken cancellationToken)
        {
            var transports = await _dbContext.Transports
                .Where(t => !t.IsCancelled &&
                    t.StartTime >= request.Day.Date + request.Shift.StartTime() &&
                    t.StartTime < request.Day.Date + request.Shift.EndTime())
                .Include(t => t.Patient)
                .ThenInclude(p => p.PersonalInformation)
                .Select(t => new
                {
                    t.Id,
                    TransportInfo = new GetTransportResponse
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
                    }
                })
                .ToDictionaryAsync(
                    keySelector: t => t.Id,
                    elementSelector: t => t.TransportInfo,
                    cancellationToken);
            
            if (transports is null)
                return Errors.Errors.MappingError;

            return new GetTransportsByDayAndShiftResponse(transports);
        }
    }
}