using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Availabilities.Queries;

/// <summary>
/// Query for getting availabilities by paramedic.
/// </summary>
/// <param name="ParamedicId">ID of paramedic as integer</param>
public record GetAvailabilitiesByParamedicQuery(int ParamedicId) : IRequest<ErrorOr<GetAvailabilitiesByParamedicResponse>>;