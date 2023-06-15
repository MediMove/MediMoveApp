using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Commands;

/// <summary>
/// Command for creating teams.
/// </summary>
/// <param name="Request">CreateTeamsRequest</param>
public record CreateTeamsCommand(CreateTeamsRequest Request) : IRequest<ErrorOr<Team[]>>;
