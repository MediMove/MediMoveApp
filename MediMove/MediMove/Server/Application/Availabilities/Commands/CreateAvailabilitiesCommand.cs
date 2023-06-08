using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Availabilities.Commands;

/// <summary>
/// Command for creating availabilities.
/// </summary>
/// <param name="ParamedicId">id of paramedic as integer</param>
/// <param name="request">CreateAvailabilitiesRequest</param>
public record CreateAvailabilitiesCommand(int ParamedicId, CreateAvailabilitiesRequest request) : IRequest<ErrorOr<Unit>>;
    