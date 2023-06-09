﻿using ErrorOr;
using MediatR;
using MediMove.Server.Application.Shared;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Extensions;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
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
                    TransportInfo = new GetTransportsByDayAndShiftResponse.TransportInfo
                    (
                        t.TeamId,
                        t.StartLocation ?? PersonalInformationToLocationConverter.Convert(t.Patient.PersonalInformation.HouseNumber, t.Patient.PersonalInformation.ApartmentNumber, t.Patient.PersonalInformation.StreetAddress, t.Patient.PersonalInformation.PostalCode, t.Patient.PersonalInformation.City),
                        t.Destination,
                        t.TransportType == TransportType.Visit ? t.ReturnLocation ?? PersonalInformationToLocationConverter.Convert(t.Patient.PersonalInformation.HouseNumber, t.Patient.PersonalInformation.ApartmentNumber, t.Patient.PersonalInformation.StreetAddress, t.Patient.PersonalInformation.PostalCode, t.Patient.PersonalInformation.City) : null,
                        t.StartTime,
                        t.Note
                    )
                })
                .ToDictionaryAsync(
                    keySelector: t => t.Id,
                    elementSelector: t => t.TransportInfo,
                    cancellationToken);
            
            if (transports is null)
                return Errors.Errors.MappingError;

            return new GetTransportsByDayAndShiftResponse
            {
                Transports = transports
            };
        }
    }
}