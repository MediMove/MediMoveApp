using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Availabilities.Queries;

/// <summary>
/// Query for getting availabilities for paramedic by date range.
/// </summary>
/// <param name="ParamedicId">ID of paramedic as integer</param>
/// <param name="StartDateInclusive">inclusive start date as nullable DateTime</param>
/// <param name="EndDateInclusive">inclusive end date as nullable DateTime</param>
public record GetAvailabilitiesForParamedicByDateRangeQuery(int ParamedicId, DateTime? StartDateInclusive, DateTime? EndDateInclusive) : IRequest<ErrorOr<GetAvailabilitiesForParamedicByDateRangeResponse>>;