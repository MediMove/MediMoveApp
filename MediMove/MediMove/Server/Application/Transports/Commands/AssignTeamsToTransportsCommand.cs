using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Commands
{
    /// <summary>
    /// Command for assigning teams to transports.
    /// </summary>
    /// <param name="Request">AssignTeamsToTransportsRequest</param>
    public record AssignTeamsToTransportsCommand(AssignTeamsToTransportsRequest Request) : IRequest<ErrorOr<Transport[]>>;
}
