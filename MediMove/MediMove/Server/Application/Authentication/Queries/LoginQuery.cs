using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Authentication.Queries
{
    public record LoginQuery(LoginUserDTO dto) : IRequest<ErrorOr<string>>;
}
