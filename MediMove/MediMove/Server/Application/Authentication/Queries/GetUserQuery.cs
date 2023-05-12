using ErrorOr;
using MediatR;
using MediMove.Server.Models;
using MediMove.Shared.Models.DTOs;

namespace MediMove.Server.Application.Authentication.Queries
{
    public record GetUserQuery(int Id) : IRequest<ErrorOr<User>>; //dla testów bez mapowania
}
