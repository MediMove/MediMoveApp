using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Availabilities.Commands;

/// <summary>
/// Command for deleting availabilities.
/// </summary>
/// <param name="ParamedicId">paramedic ID as integer</param>
/// <param name="Request">DeleteAvailabilitiesRequest</param>
public record DeleteAvailabilitiesCommand(int ParamedicId, DeleteAvailabilitiesRequest Request) : IRequest<ErrorOr<Unit>>;