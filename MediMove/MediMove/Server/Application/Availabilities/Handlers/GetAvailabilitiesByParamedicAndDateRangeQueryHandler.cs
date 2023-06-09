﻿using ErrorOr;
using MediatR;
using MediMove.Server.Application.Availabilities.Queries;
using MediMove.Server.Data;
using MediMove.Shared.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MediMove.Server.Application.Availabilities.Handlers
{
    /// <summary>
    /// Handler for getting availabilities by paramedic and date range.
    /// </summary>
    public class GetAvailabilitiesByParamedicAndDateRangeQueryHandler : IRequestHandler<GetAvailabilitiesByParamedicAndDateRangeQuery, ErrorOr<GetAvailabilitiesForParamedicByDateRangeResponse>>
    {
        private readonly MediMoveDbContext _dbContext;

        /// <summary>
        /// Constructor for GetAvailabilitiesByParamedicAndDateRangeQueryHandler.
        /// </summary>
        /// <param name="dbContext">dbContext to inject</param>
        public GetAvailabilitiesByParamedicAndDateRangeQueryHandler(MediMoveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Method for handling the GetAvailabilitiesByParamedicAndDateRangeQuery.
        /// </summary>
        /// <param name="query">GetAvailabilitiesByParamedicAndDateRangeQuery</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>GetAvailabilitiesByParamedicAndDateRangeResponse wrapped in ErrorOr</returns>
        public async Task<ErrorOr<GetAvailabilitiesForParamedicByDateRangeResponse>> Handle(GetAvailabilitiesByParamedicAndDateRangeQuery query, CancellationToken cancellationToken)
        {
            var teams = await _dbContext.Teams
                .Where(t => (t.DriverId == query.ParamedicId || t.ParamedicId == query.ParamedicId) &&
                    (!query.StartDateInclusive.HasValue || t.Day.Date >= query.StartDateInclusive.Value.Date) &&
                    (!query.EndDateInclusive.HasValue || t.Day.Date <= query.EndDateInclusive.Value.Date))
                .Select(t => new
                {
                    t.Day.Date,
                    t.ShiftType
                })
                .ToArrayAsync(cancellationToken);

            var availabilities = await _dbContext.Availabilities
                .Where(a => a.ParamedicId == query.ParamedicId &&
                    (!query.StartDateInclusive.HasValue || a.Day.Date >= query.StartDateInclusive.Value.Date) &&
                    (!query.EndDateInclusive.HasValue || a.Day.Date <= query.EndDateInclusive.Value.Date))
                .Select(a => new
                {
                    a.Day.Date,
                    a.ShiftType
                })
                .ToArrayAsync(cancellationToken);

            var dateToDateInfo = availabilities.Select(a => new
            {
                a.Date,
                DeclaredShift = a.ShiftType,
                TeamShift = teams.SingleOrDefault(t => t.Date == a.Date)?.ShiftType
            })
            .ToDictionary(r => r.Date,
                r => new GetAvailabilitiesForParamedicByDateRangeResponse.DateInfo
                (
                    r.DeclaredShift,
                    r.TeamShift
                )
            );

            return new GetAvailabilitiesForParamedicByDateRangeResponse
            {
                DateToDateInfo = dateToDateInfo
            };
        }
    }
}