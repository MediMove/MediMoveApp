using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Teams.Commands
{
    /// <summary>
    /// Command for deleting teams.
    /// </summary>
    /// <param name="Request">DeleteTeamsRequest</param>
    public record DeleteTeamsCommand(DeleteTeamsRequest Request) : IRequest<ErrorOr<Unit>>;
}
