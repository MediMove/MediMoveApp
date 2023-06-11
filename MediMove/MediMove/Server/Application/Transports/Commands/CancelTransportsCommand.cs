using ErrorOr;
using MediatR;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Transports.Commands
{
    /// <summary>
    /// Command for cancelling transports.
    /// </summary>
    /// <param name="Request">CancelTransportsRequest</param>
    public record CancelTransportsCommand(CancelTransportsRequest Request) : IRequest<ErrorOr<Unit>>;
}
