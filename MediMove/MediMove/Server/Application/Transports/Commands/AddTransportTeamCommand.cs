using ErrorOr;
using MediatR;
using MediMove.Server.Models;

namespace MediMove.Server.Application.Transports.Commands
{
    public record AddTransportTeamCommand(int TransportId, int TeamId) : IRequest<ErrorOr<Transport>>;
}
