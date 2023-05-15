using ErrorOr;
using MediatR;

namespace MediMove.Server.Application.Teams.Commands
{
    public record ChangeWorkingStateCommand(int Id, bool state) : IRequest<ErrorOr<Unit>>; // do zaktualizowania return type
}
