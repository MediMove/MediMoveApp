using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Commands
{
    /// <summary>
    /// Command for deleting transports.
    /// </summary>
    /// <param name="Request"></param>
    public record DeleteTransportsCommand(DeleteTransportsRequest Request) : IRequest<ErrorOr<Unit>>;
}
