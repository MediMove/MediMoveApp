using ErrorOr;
using MediatR;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;
using MediMove.Shared.Models.DTOs.temp;

namespace MediMove.Server.Application.Authentication.Commands
{
    public record RegisterUserCommand(RegisterUserDTO dto) : IRequest<ErrorOr<int>>;
}
