using ErrorOr;
using MediatR;
using MediMove.Server.Application.Models;

namespace MediMove.Server.Application.Authentication.Commands;

/// <summary>
/// Command for registering a user.
/// </summary>
/// <param name="Email">email as string</param>
/// <param name="Password">password as string</param>
/// <param name="AccountId">id of related entity</param>
/// <param name="RoleName">role name</param>
public record RegisterUserCommand(string Email, string Password, int? AccountId, string RoleName) : IRequest<ErrorOr<User>>;