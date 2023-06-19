using AutoMapper;
using ErrorOr;
using MediatR;
using MediMove.Server.Application.Shared;
using MediMove.Server.Application.Transports.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Radzen.Blazor.Rendering;

namespace MediMove.Server.Application.Transports.Handlers
{
    /// <summary>
    /// Handler for getting transports by paramedic and date range.
    /// </summary>
    public class GetTransportsByParamedicAndDateRangeQueryHandler : IRequestHandler<GetTransportsByParamedicAndDateRangeQuery, ErrorOr<GetTransportsByParamedicAndDateRangeResponse>>
    {
        private readonly IMapper _mapper;
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetTransportsByParamedicAndDateRangeQueryHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public GetTransportsByParamedicAndDateRangeQueryHandler(IMapper mapper, MediMoveDbContext dbContext)
        {
            _mapper = mapper;
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
            var transportInfos = await _dbContext.Transports
                .Where(t => !t.IsCancelled &&
                    (!query.StartDateInclusive.HasValue || t.StartTime.Date >= query.StartDateInclusive.Value.Date) &&
                    (!query.EndDateInclusive.HasValue || t.StartTime.Date <= query.EndDateInclusive.Value.Date) &&
                    (t.Team.DriverId == query.ParamedicId || t.Team.ParamedicId == query.ParamedicId))
                .Select(t => new GetTransportsByParamedicAndDateRangeResponse.TransportInfo(
                    t.Patient.PersonalInformation.FirstName,
                    t.Patient.PersonalInformation.LastName,
                    t.Patient.PersonalInformation.PhoneNumber,
                    t.Patient.Weight,
                    t.StartLocation ?? PersonalInformationToLocationConverter.Convert(t.Patient.PersonalInformation.HouseNumber, t.Patient.PersonalInformation.ApartmentNumber, t.Patient.PersonalInformation.StreetAddress, t.Patient.PersonalInformation.PostalCode, t.Patient.PersonalInformation.City),
                    t.Destination,
                    t.TransportType == TransportType.Visit ? t.ReturnLocation ?? PersonalInformationToLocationConverter.Convert(t.Patient.PersonalInformation.HouseNumber, t.Patient.PersonalInformation.ApartmentNumber, t.Patient.PersonalInformation.StreetAddress, t.Patient.PersonalInformation.PostalCode, t.Patient.PersonalInformation.City) : null,
                    t.StartTime,
                    t.Financing,
                    t.PatientPosition,
                    t.Note)
                ).ToListAsync(cancellationToken);

            var DateToTransportInfos = transportInfos
                .GroupBy(t => t.StartTime.Date)
                .ToDictionary(
                    x => x.Key,
                    x => x.ToArray()
                );


            return new GetTransportsByParamedicAndDateRangeResponse
            {
                Transports = DateToTransportInfos
            };
        }
    }
}